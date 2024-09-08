// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using HospitalConsoleApplication;

class Program
{
    public static void Main(string[] args)
    {
        List<Patient> patients;
        List<Doctor> doctors;
        
        doctors = FileManager.LoadDoctors();
        patients = FileManager.LoadPatients(doctors);
        
        Console.WriteLine("┌──────────────────────────────────────────────────┐");
        Console.WriteLine("│        DOTNET Hospital Management System         │");
        Console.WriteLine("├──────────────────────────────────────────────────┤");
        Console.WriteLine("│                     Login                        │");
        Console.WriteLine("└──────────────────────────────────────────────────┘");

        object? user = null;

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
            case Patient:
                Console.WriteLine("patient");
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

    public static object? Authentication(string id, string password, List<Patient> patients, List<Doctor> doctors)
    {
        var patient = patients.Find(p => p.Id.ToString() == id && p.Password == password);
        if (patient != null) return patient;
        
        var doctor = doctors.Find(d => d.Id.ToString() == id && d.Password == password);
        if (doctor != null) return doctor;

        return null;   
    }
    


}





// doctors.Add(new Doctor("doc", "23214", "doc@gmail.com", "asdij", 24234));
// patients.Add(new Patient("alex", "123", "alex@gmail.com", "nicholas av", 098334, doctors.First()));

// FileManager.SavePatients(patients);
// FileManager.SaveDoctors(doctors);