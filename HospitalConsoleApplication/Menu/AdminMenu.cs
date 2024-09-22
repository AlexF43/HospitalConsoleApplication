namespace HospitalConsoleApplication;

public class AdminMenu
{
    private List<Doctor> _doctors;
    private List<Patient> _patients;

    // inialise all data for the admin screens
    public AdminMenu(List<Doctor> doctors, List<Patient> patients)
    {
        _doctors = doctors;
        _patients = patients;
    }

    // admin home screen
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
        Console.WriteLine("7. Seed data into application");
        Console.WriteLine("8. Logout");
        Console.WriteLine("9. Exit");

        // get and validate user input
        int optionInt = 0;
        bool validInput;
        do
        {
            string? option = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(option) || !int.TryParse(option, out optionInt))
            {
                validInput = false;
            }
            else
            {
                validInput = (optionInt >= 1 && optionInt <= 9);
            }

            if (!validInput)
            {
                Console.WriteLine("Invalid Input, Please enter a number between 1 and 9");
            }
        } while (!validInput);

        // display the selected screen
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
                SeedData();
                break;

            case 8:
                MainMenu.DisplayMainMenu();
                break;

            case 9:
                _patients = null;
                _doctors = null;
                GC.Collect();
                Environment.Exit(1);
                break;

            default:
                Console.WriteLine("Invalid input, please try again");
                DisplayAdminMenu();
                break;
        }
    }

    // list all the doctors in the system
    private void ListDoctors()
    {
        Console.Clear();
        Utils.PageHeader("All Doctors");
        Console.WriteLine();
        if (_doctors.Count != 0)
        {
            Utils.DoctorHeader();
            foreach (var doctor in _doctors)
            {
                doctor.DisplayDetails();
            }
        }
        else
        {
            Console.WriteLine("There are no doctors registered to the application");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();
    }

    // search doctors by their id and return their infomation
    private void SearchDoctorDetails()
    {
        Console.Clear();
        Utils.PageHeader("Doctor Details");
        Console.WriteLine();
        Console.Write("Enter the Id of the doctor who's details you are checking, or enter n to exit to the menu: ");
        string input;
        Doctor doctor = null;
        do
        {
            input = Console.ReadLine();

            if (input.ToLower() == "n")
            {
                DisplayAdminMenu();
                return;
            }

            if (int.TryParse(input, out int id))
            {
                doctor = _doctors.Find(d => d.Id == id);
                if (doctor == null)
                {
                    Console.WriteLine("Not a valid id, please try again");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, ID's are only integers");
            }
        } while (doctor == null);

        Console.WriteLine();
        Utils.DoctorHeader();
        doctor.DisplayDetails();
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();
    }

    // list all the patients in the application
    private void ListPatients()
    {
        Console.Clear();
        Utils.PageHeader("All Patients");
        Console.WriteLine();
        if (_patients.Count != 0)
        {
            Utils.PatientHeader();
            foreach (var patient in _patients)
            {
                patient.DisplayDetails();
            }
        }
        else
        {
            Console.WriteLine("There are no patients currently registered to the application");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();
    }

    // allow searching of patients by their id and displaying of their infomation
    private void SearchPatientDetails()
    {
        Console.Clear();
        Utils.PageHeader("Patient Details");
        Console.WriteLine();
        Console.Write("Enter the Id of the patient who's details you are checking, or enter n to exit to the menu: ");
        string input;
        Patient patient = null;
        do
        {
            input = Console.ReadLine();

            if (input.ToLower() == "n")
            {
                DisplayAdminMenu();
                return;
            }

            if (int.TryParse(input, out int id))
            {
                patient = _patients.Find(p => p.Id == id);
                if (patient == null)
                {
                    Console.WriteLine("Not a valid id, please try again");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, ID's are only integers");
            }
        } while (patient == null);


        Console.WriteLine();
        Utils.PatientHeader();
        patient.DisplayDetails();
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the admin menu");
        Console.ReadKey();
        DisplayAdminMenu();
    }

    // screen to add a new doctor to the system
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
            Console.Write("Phone Number: ");
            string? phoneNumber = Console.ReadLine();

            if (!UserValidation.ValidateInput(name, s => !string.IsNullOrWhiteSpace(s), "Name cannot be empty.") ||
                !UserValidation.ValidateInput(password, s => !string.IsNullOrWhiteSpace(s),
                    "Password cannot be empty.") ||
                !UserValidation.ValidateInput(email, s => s.IsValidEmail(), "Invalid email address.") ||
                !UserValidation.ValidateInput(address, s => !string.IsNullOrWhiteSpace(s),
                    "Address cannot be empty.") ||
                !UserValidation.ValidateInput(phoneNumber, s => int.TryParse(s, out phoneNumberInt),
                    "Invalid phone number. please only use valid digits"))
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

    // screen to add a new patient to the system
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
            Console.Write("Phone Number: ");
            string? phoneNumber = Console.ReadLine();

            if (!UserValidation.ValidateInput(name, s => !string.IsNullOrWhiteSpace(s), "Name cannot be empty.") ||
                !UserValidation.ValidateInput(password, s => !string.IsNullOrWhiteSpace(s),
                    "Password cannot be empty.") ||
                !UserValidation.ValidateInput(email, s => s.IsValidEmail(), "Invalid email address.") ||
                !UserValidation.ValidateInput(address, s => !string.IsNullOrWhiteSpace(s),
                    "Address cannot be empty.") ||
                !UserValidation.ValidateInput(phoneNumber, s => int.TryParse(s, out phoneNumberInt),
                    "Invalid phone number. please only use valid digits"))
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

    // seeds the application with an example patient and doctor for testing purposes
    private void SeedData()
    {
        Console.Clear();
        Utils.PageHeader("Seed data");
        Console.WriteLine();
        if (_patients.Find(p => p.Id == 2) == null && _doctors.Find(d => d.Id == 3) == null)
        {
            _patients.Add(new Patient(2, "Alex", "1234", "alex@gmail.com", "43 Real Street", 1234567));
            FileManager.SavePatients(_patients);
            _doctors.Add(new Doctor(3, "Dr John", "qwerty", "John@drmail.com", "76 Bond Street", 0987654));
            FileManager.SaveDoctors(_doctors);
            Console.WriteLine("Seeded data into the applicaion");
        }
        else
        {
            Console.WriteLine("There is already similar data in the application");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        DisplayAdminMenu();
    }
}