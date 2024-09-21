namespace HospitalConsoleApplication;

public class PatientMenu
{
    private Patient patient;

    public PatientMenu(Patient patient)
    {
        this.patient = patient;
    }

    public void DisplayPatientMenu()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the DOTNET hospital management system {patient.Name}");
        Console.WriteLine("");
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. List patient details");
        Console.WriteLine("2. List my doctor details");
        Console.WriteLine("3. List all appointments");
        Console.WriteLine("4. Book appointment");
        Console.WriteLine("5. Exit to login");
        Console.WriteLine("6. Exit system");

        string option = Console.ReadLine();
        int optionInt = int.Parse(option);
        switch (optionInt)
        {
            case 1: 
                DisplayPatientDetails(); 
                break;
            
            case 2:
                DisplayDoctorDetails();
                break;
            
            case 3:
                Console.WriteLine("appointment menu");
                break;
            case 4:
                Console.WriteLine("Book Appointment");
                break;
            case 5:
                MainMenu.DisplayMainMenu();
                break;
            
            case 6:
                break;
            
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    public void DisplayPatientDetails()
    {
        Console.WriteLine($"{patient.Name}'s Details");
        Console.WriteLine("");
        Console.WriteLine($"Patient ID {patient.Id}");
        Console.WriteLine($"Full Name {patient.Name}");
        Console.WriteLine($"Address: {patient.Address}");
        Console.WriteLine($"Email: {patient.Email}");
        Console.WriteLine($"Phone Number: {patient.PhoneNumber}");
        Console.ReadLine();
        DisplayPatientMenu();
    }

    public void DisplayDoctorDetails()
    {
        Console.WriteLine("Your Doctor:");
        Console.WriteLine("--");
        Console.Write("Name");
        Utils.WriteSpaces(16);
        Console.Write("| ");
        Console.Write("Email");
        Utils.WriteSpaces(15);
        Console.Write("| ");
        Console.Write("Phone number");
        Utils.WriteSpaces(1);
        Console.Write("| ");
        Console.Write("Address");
        Utils.WriteSpaces(22);
        Console.WriteLine();
        Utils.WriteDashes(90);
        Console.WriteLine();
        patient.Doctor.DisplayDetails();
        Console.ReadLine();
        DisplayPatientMenu();
    }
}