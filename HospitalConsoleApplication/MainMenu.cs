namespace HospitalConsoleApplication;

static class MainMenu
{
    static List<Patient> patients;
    static List<Doctor> doctors;

    static MainMenu()
    {
        LoadData();
    }

    public static void DisplayMainMenu()
    {
        Console.WriteLine("┌──────────────────────────────────────────────────┐");
        Console.WriteLine("│        DOTNET Hospital Management System         │");
        Console.WriteLine("├──────────────────────────────────────────────────┤");
        Console.WriteLine("│                     Login                        │");
        Console.WriteLine("└──────────────────────────────────────────────────┘");

        User? user = null;

        do
        {
            Console.WriteLine();
            Console.Write("ID: ");
            string id = Console.ReadLine();
            Console.Write("Password: ");
            string password = ReadPassword();
            Console.WriteLine(id + password);

            user = Authentication(id, password, patients, doctors);
            if (user == null)
            {
                Console.WriteLine("Invalid ID or password. Please try again.");
            }
        } while (user == null);

        switch (user)
        {
            case Doctor:
                //Doctor ui
                Console.WriteLine("doctor");
                break;
            
            case Patient patient:
                PatientMenu patientMenu = new PatientMenu(patient);
                patientMenu.DisplayPatientMenu();
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

            if (key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        } 
        while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }

    public static User? Authentication(string id, string password, List<Patient> patients, List<Doctor> doctors)
    {
        var patient = patients.Find(p => p.Id.ToString() == id && p.Password == password);
        if (patient != null) return patient;
        
        var doctor = doctors.Find(d => d.Id.ToString() == id && d.Password == password);
        if (doctor != null) return doctor;

        return null;   
    }

    static void LoadData()
    {
        doctors = FileManager.LoadDoctors();
        patients = FileManager.LoadPatients(doctors);
    }
}