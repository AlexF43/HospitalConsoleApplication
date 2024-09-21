namespace HospitalConsoleApplication;

public class DoctorMenu
{
    private Doctor doctor;
    private List<Patient> _patients;

    public DoctorMenu(Doctor doctor, List<Patient> patients)
    {
        this.doctor = doctor;
        _patients = patients;
    }

    public void DisplayDoctorMenu()
    {
        Console.Clear();
        Utils.PageHeader("Doctor Menu");
        Console.WriteLine($"Welcome to the DOTNET hospital management system {doctor.Name}");
        Console.WriteLine("");
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. List doctor details");
        Console.WriteLine("2. List patients");
        Console.WriteLine("3. List appointments");
        Console.WriteLine("4. Check particular patient");
        Console.WriteLine("5. List appointments with patient");
        Console.WriteLine("6. Logout");
        Console.WriteLine("7. Exit");

        int optionInt;
        Boolean validInput;
        do
        {
            string option = Console.ReadLine();
            optionInt = int.Parse(option);
            validInput = (optionInt <= 7 && optionInt >= 1);
            if (!validInput)
            {
                Console.WriteLine("Invalid Input, Please enter a number between 1 and 7");
            }
        } while (!validInput);

        switch (optionInt)
        {
            case 1:
                DisplayDoctorDetails();
                break;

            case 2:
                DisplayListOfPatients();
                break;

            case 3:
                Console.WriteLine("appointment menu");
                break;
            case 4:
                Console.WriteLine("Book Appointment");
                break;
            case 5:
                break;

            case 6:
                MainMenu.DisplayMainMenu();
                break;

            case 7:
                break;

            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private void DisplayDoctorDetails()
    {
        Utils.PageHeader("My Details");
        Utils.DoctorHeader();
        doctor.DisplayDetails();
        Console.ReadLine();
        DisplayDoctorMenu();
    }

    private void DisplayListOfPatients()
    {
        Utils.PageHeader("My Patients");
        List<Patient> currentDoctorsPatients = _patients
            .Where(patient => patient.Doctor != null && patient.Doctor.Id == doctor.Id)
            .ToList();
    
        if (currentDoctorsPatients.Count == 0)
        {
            Console.WriteLine("No patients found for the current doctor.");
        }
        else
        {
            Utils.PatientHeader();
            foreach (var patient in currentDoctorsPatients) 
            {
                patient.DisplayDetails();
            }
        }
    }
}