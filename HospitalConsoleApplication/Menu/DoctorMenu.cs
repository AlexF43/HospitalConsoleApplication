namespace HospitalConsoleApplication;

public class DoctorMenu
{
    private Doctor _doctor;
    private List<Patient> _patients;
    private List<Appointment> _appointments;

    // inisialise all data for the doctors screens
    public DoctorMenu(Doctor doctor, List<Patient> patients, List<Appointment> appointments)
    {
        _doctor = doctor;
        _patients = patients;
        _appointments = appointments;
    }

    // doctor menu home screen
    public void DisplayDoctorMenu()
    {
        Console.Clear();
        Utils.PageHeader("Doctor Menu");
        Console.WriteLine($"Welcome to the DOTNET hospital management system {_doctor.Name}");
        Console.WriteLine("");
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. List doctor details");
        Console.WriteLine("2. List patients");
        Console.WriteLine("3. List appointments");
        Console.WriteLine("4. Check particular patient");
        Console.WriteLine("5. List appointments with patient");
        Console.WriteLine("6. Logout");
        Console.WriteLine("7. Exit");

        // get and validate the user input
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
                validInput = (optionInt >= 1 && optionInt <= 7);
            }

            if (!validInput)
            {
                Console.WriteLine("Invalid Input, Please enter a number between 1 and 7");
            }
        } while (!validInput);

        // display the selected next screen
        switch (optionInt)
        {
            case 1:
                DisplayDoctorDetails();
                break;

            case 2:
                DisplayListOfPatients();
                break;

            case 3:
                ListAppointments();
                break;
            case 4:
                SearchPatientDetails();
                break;
            case 5:
                ListAppointmentsWithPatient();
                break;

            case 6:
                MainMenu.DisplayMainMenu();
                break;

            case 7:
                _appointments = null;
                _doctor = null;
                _patients = null;
                GC.Collect();
                Environment.Exit(1);
                break;

            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    // display the doctors own details
    private void DisplayDoctorDetails()
    {
        Console.Clear();
        Utils.PageHeader("My Details");
        Console.WriteLine();
        Utils.DoctorHeader();
        _doctor.DisplayDetails();
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the doctor menu");
        Console.ReadKey();
        DisplayDoctorMenu();
    }

    // display a list of the patients assigned to a doctor
    private void DisplayListOfPatients()
    {
        Console.Clear();
        Utils.PageHeader("My Patients");
        List<Patient> currentDoctorsPatients = _patients
            .Where(patient => patient.Doctor != null && patient.Doctor.Id == _doctor.Id)
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

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the doctor menu");
        Console.ReadKey();
        DisplayDoctorMenu();
    }
    
    // display all the appointments a doctor is involved with
    private void ListAppointments()
    {
        Console.Clear();
        Utils.PageHeader("All Appointments");
        Console.WriteLine();
        Utils.AppointmentHeader();
        List<Appointment> doctorAppointments = _appointments
            .Where(appointment => appointment.Doctor.Id == _doctor.Id)
            .ToList();
        foreach (var appointment in doctorAppointments)
        {
            appointment.DisplayDetails();
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the doctor menu");
        Console.ReadKey();
        DisplayDoctorMenu();
    }

    // screen for a doctor to search a user by ID and see their details
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
                DisplayDoctorMenu();
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
        DisplayDoctorMenu();
    }

    // screen for a doctor to search a particular patient and see all their appointments with that patient
    private void ListAppointmentsWithPatient()
    {
        Console.Clear();
        Utils.PageHeader("View Appointments with Patient");
        Console.WriteLine();
        Console.Write("Enter the Id of the patient whose appointments you want to view, or enter n to exit to the menu: ");
        string input;
        Patient patient = null;
        do
        {
            input = Console.ReadLine();

            if (input.ToLower() == "n")
            {
                DisplayDoctorMenu();
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
        Utils.AppointmentHeader();
        var patientAppointments = _appointments
            .Where(a => a.Patient.Id == patient.Id && a.Doctor.Id == _doctor.Id)
            .ToList();
    
        if (patientAppointments.Any())
        {
            foreach (var appointment in patientAppointments)
            {
                appointment.DisplayDetails();
            }
        }
        else
        {
            Console.WriteLine($"No appointments found with {patient.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the doctor menu");
        Console.ReadKey();
        DisplayDoctorMenu();
    }
}