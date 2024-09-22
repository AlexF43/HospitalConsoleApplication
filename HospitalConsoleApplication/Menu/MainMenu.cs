namespace HospitalConsoleApplication;

static class MainMenu
{
    // lists of objects to be populated by the LoadData() function
    static List<Patient> patients;
    static List<Doctor> doctors;
    static List<Administrator> admins;
    static List<Appointment> appointments;

    // constructor to initialise the data when the class is first used
    static MainMenu()
    {
        LoadData();
    }

    // main menu screen
    public static void DisplayMainMenu()
    {
        Console.Clear();
        Utils.PageHeader("Login");

        BaseUser? user = null;
        
        // get and validate user input
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

        // depending on the type of user logging in, open their respecting screen
        switch (user)
        {
            case Doctor doctor:
                DoctorMenu doctorMenu = new DoctorMenu(doctor, patients, appointments);
                doctorMenu.DisplayDoctorMenu();
                break;
            
            case Patient patient:
                PatientMenu patientMenu = new PatientMenu(patient, appointments, doctors, patients);
                patientMenu.DisplayPatientMenu();
                break;
            
            case Administrator admin:
                AdminMenu adminMenu = new AdminMenu(doctors, patients);
                adminMenu.DisplayAdminMenu();
                break;
            
            default:
                Console.WriteLine("unrecognised user type");
                break;
        }
    }
    
    // get the entered password and replace all charactors with a *
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

    // authenticate a users login attempt and return their class
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
        // attempt to load data
        try
        {
            doctors = FileManager.LoadDoctors();
            patients = FileManager.LoadPatients(doctors);
            admins = FileManager.LoadAdministrators();
            appointments = FileManager.LoadAppointments(doctors, patients);

            // if there are no adminstrators, add an example one to be used
            if (admins.Count == 0)
            {
                admins.Add(new Administrator(1, "password"));
                FileManager.SaveAdministrators(admins);
            }
        }
        // if errors appear, catch them and exit the program safely
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}