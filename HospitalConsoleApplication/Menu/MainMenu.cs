namespace HospitalConsoleApplication;

static class MainMenu
{
    static List<Patient> patients;
    static List<Doctor> doctors;
    static List<Administrator> admins;
    static List<Appointment> appointments;

    static MainMenu()
    {
        LoadData();
    }

    public static void DisplayMainMenu()
    {
        Console.Clear();
        Utils.PageHeader("Login");

        BaseUser? user = null;

        do
        {
            Console.WriteLine();
            Console.Write("ID: ");
            string id = Console.ReadLine();
            Console.Write("Password: ");
            string password = ReadPassword();

            user = Authentication(id, password, patients, doctors);
            if (user == null)
            {
                Console.WriteLine("Invalid ID or password. Please try again.");
            }
        } while (user == null);

        switch (user)
        {
            case Doctor doctor:
                //Doctor ui
                DoctorMenu doctorMenu = new DoctorMenu(doctor, patients, appointments);
                doctorMenu.DisplayDoctorMenu();
                break;
            
            case Patient patient:
                PatientMenu patientMenu = new PatientMenu(patient, appointments, doctors, patients);
                patientMenu.DisplayPatientMenu();
                break;
            
            case Administrator admin:
                AdminMenu adminMenu = new AdminMenu(admin, doctors, patients, appointments);
                adminMenu.DisplayAdminMenu();
                break;
            
            default:
                Console.WriteLine("unrecognised user type");
                break;
        }
    }
    
    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        } 
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }

    public static BaseUser? Authentication(string id, string password, List<Patient> patients, List<Doctor> doctors)
    {
        // make sure id is an int
        if (!int.TryParse(id, out int userId))
        {
            return null;
        }
        
        // find if patient
        var patient = patients.Find(delegate(Patient p)
        {
            return p.Id.ToString() == id && p.Password == password;
        });
        if (patient != null) return patient;
        
        // find if doctor
        var doctor = doctors.Find(delegate(Doctor d)
        {
            return d.Id.ToString() == id && d.Password == password;
        });
        if (doctor != null) return doctor;
        
        // find if admin
        var admin = admins.Find(delegate(Administrator a)
        {
            return a.Id.ToString() == id && a.Password == password;
        });
        if (admin != null) return admin;

        return null;   
    }

    static void LoadData()
    {
        try
        {
            doctors = FileManager.LoadDoctors();
            patients = FileManager.LoadPatients(doctors);
            admins = FileManager.LoadAdministrators();
            appointments = FileManager.LoadAppointments(doctors, patients);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}