namespace HospitalConsoleApplication;

public class AdminMenu
{
    private Administrator _admin;
    private List<Doctor> _doctors;
    private List<Patient> _patients;
    private List<Appointment> _appointments;

    public AdminMenu(Administrator admin, List<Doctor> doctors, List<Patient> patients, List<Appointment> appointments)
    {
        _admin = admin;
        _doctors = doctors;
        _patients = patients;
        _appointments = appointments;
    }
    
    public void DisplayAdminMenu()
    {
        Console.Clear();
        Utils.PageHeader("Administrator Menu");
        Console.WriteLine($"Welcome to the DOTNET hospital management system");
        Console.WriteLine("");
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. List all doctors");
        Console.WriteLine("2. Check doctor details");
        Console.WriteLine("3. list all patients");
        Console.WriteLine("4. Check patient details");
        Console.WriteLine("5. Add doctor");
        Console.WriteLine("6. Add patient");
        Console.WriteLine("7. Logout");
        Console.WriteLine("8. Exit");

        int optionInt;
        Boolean validInput;
        do
        {
            string? option = Console.ReadLine();
            optionInt = int.Parse(option);
            validInput = (optionInt <= 8 && optionInt >= 1);
            if (!validInput)
            {
                Console.WriteLine("Invalid Input, Please enter a number between 1 and 8");
            }
        } while (!validInput);

        switch (optionInt)
        {
            case 1:
                ListDoctors();
                break;

            case 2:
                SearchDoctorDetails();
                break;

            case 3:
                ListPatients();
                break;
            case 4:
                SearchPatientDetails();
                break;
            case 5:
                AddDoctor();
                break;
            
            case 6:
                AddPatient();
                break;

            case 7:
                MainMenu.DisplayMainMenu();
                break;

            case 8:
                _appointments = null;
                _patients = null;
                _admin = null;
                _doctors = null;
                GC.Collect();
                Environment.Exit(1);
                break;

            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private void ListDoctors()
    {
        Console.Clear();
        Utils.PageHeader("All Doctors");
        Console.WriteLine();
        Utils.DoctorHeader();
        foreach (var doctor in _doctors)
        {
            doctor.DisplayDetails();
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();;
    }

    private void SearchDoctorDetails()
    {
        Console.Clear();
        Utils.PageHeader("Doctor Details");
        Console.WriteLine();
        Console.Write("Enter the Id of the doctor who's details you are checking, or enter n to exit to the menu: ");
        string input;
        Doctor? doctor;
        do
        {
            input = Console.ReadLine();
            var id = int.Parse(input);
            doctor = _doctors.Find(d => d.Id == id);
            if (doctor == null && input != "n")
            {
                Console.WriteLine("Not a valid ID, Please try again");
            }
        } while (doctor == null);

        if (input == "n")
        {
            DisplayAdminMenu();
        }
        else
        {
            Console.WriteLine();
            Utils.PatientHeader();
            doctor.DisplayDetails();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the admin menu");
            Console.ReadKey();
            DisplayAdminMenu();
        }
    }
    
    private void ListPatients()
    {
        Console.Clear();
        Utils.PageHeader("All Patients");
        Console.WriteLine();
        Utils.PatientHeader();
        foreach (var patient in _patients)
        {
            patient.DisplayDetails();
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();
    }
    
    private void SearchPatientDetails()
    {
        Console.Clear();
        Utils.PageHeader("Patient Details");
        Console.WriteLine();
        Console.Write("Enter the Id of the patient who's details you are checking, or enter n to exit to the menu: ");
        string input;
        Patient? patient;
        do
        {
            input = Console.ReadLine();
            var id = int.Parse(input);
            patient = _patients.Find(d => d.Id == id);
            if (patient == null && input != "n")
            {
                Console.WriteLine("Not a valid ID, Please try again");
            }
        } while (patient == null);

        if (input == "n")
        {
            DisplayAdminMenu();
        }
        else
        {
            Console.WriteLine();
            Utils.PatientHeader();
            patient.DisplayDetails();
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the admin menu");
            Console.ReadKey();
            DisplayAdminMenu();
        }
    }

    private void AddDoctor()
{
    Console.Clear();
    Utils.PageHeader("Add Doctor");
    Console.WriteLine();
    Console.WriteLine("Registering a doctor with the hospital management system");
    string name;
    string password;
    string email;
    string address;
    int phoneNumberInt = 0;
    
    do
    {
        Console.Write("Name: ");
        name = Console.ReadLine();
        Console.Write("Password: ");
        password = Console.ReadLine();
        Console.Write("Email: ");
        email = Console.ReadLine();
        Console.Write("Address: ");
        address = Console.ReadLine();
        Console.Write("Phone Number:");
        string? phoneNumber = Console.ReadLine();
        
        if (!UserValidation.ValidateInput(name, s => !string.IsNullOrWhiteSpace(s), "Name cannot be empty.") ||
            !UserValidation.ValidateInput(password, s => !string.IsNullOrWhiteSpace(s), "Password cannot be empty.") ||
            !UserValidation.ValidateInput(email, s => s.IsValidEmail(), "Invalid email address.") ||
            !UserValidation.ValidateInput(address, s => !string.IsNullOrWhiteSpace(s), "Address cannot be empty.") ||
            !UserValidation.ValidateInput(phoneNumber, s => int.TryParse(s, out phoneNumberInt), "Invalid phone number. please only use valid digits"))
        {
            Console.WriteLine("Please try again.");
            continue;
        }
        
        break;
    } while (true);

    Doctor newDoctor = new Doctor(name, password, email, address, phoneNumberInt);
    _doctors.Add(newDoctor);
    
    try
    {
        FileManager.SaveDoctors(_doctors);
        Console.WriteLine($"Added {newDoctor.Name} to the system");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine("The doctor was unable to be saved");
    }

    Console.WriteLine("Press any key to return to the admin menu");
    Console.ReadKey();
    DisplayAdminMenu();
}
    
    private void AddPatient()
    {
        Console.Clear();
        Utils.PageHeader("Add Patient");
        Console.WriteLine();
        Console.WriteLine("Registering a patient with the hospital management system");
        bool valid = false;
        string name;
        string password;
        string email;
        string address;
        int phoneNumberInt = 0;
        
        do
        {
            Console.Write("Name: ");
            name = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.Write("Address: ");
            address = Console.ReadLine();
            Console.Write("Phone Number:");
            string? phoneNumber = Console.ReadLine();
            
            if (!UserValidation.ValidateInput(name, s => !string.IsNullOrWhiteSpace(s), "Name cannot be empty.") ||
                !UserValidation.ValidateInput(password, s => !string.IsNullOrWhiteSpace(s), "Password cannot be empty.") ||
                !UserValidation.ValidateInput(email, s => s.IsValidEmail(), "Invalid email address.") ||
                !UserValidation.ValidateInput(address, s => !string.IsNullOrWhiteSpace(s), "Address cannot be empty.") ||
                !UserValidation.ValidateInput(phoneNumber, s => int.TryParse(s, out phoneNumberInt), "Invalid phone number. please only use valid digits"))
            {
                Console.WriteLine("Please try again.");
                continue;
            }
            
            break;
        } while (true);

        Patient newPatient = new Patient(name, password, email, address, phoneNumberInt);
        _patients.Add(newPatient);
        
        try
        {
            FileManager.SavePatients(_patients);
            Console.WriteLine($"Added {newPatient.Name} to the system");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("The patient was unable to be saved");
        }

        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();
    }
}