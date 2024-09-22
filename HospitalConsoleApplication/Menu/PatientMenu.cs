namespace HospitalConsoleApplication;

public class PatientMenu
{
    private Patient _patient;
    private List<Patient> _patients;
    private List<Appointment> _appointments;
    private List<Doctor> _doctors;

    public PatientMenu(Patient patient, List<Appointment> appointments, List<Doctor> doctors, List<Patient> patients)
    {
        _patient = patient;
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public void DisplayPatientMenu()
    {
        Console.Clear();
        
        Utils.PageHeader("Patient Menu");
        Console.WriteLine($"Welcome to the DOTNET hospital management system {_patient.Name}");
        Console.WriteLine("");
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. List patient details");
        Console.WriteLine("2. List my doctor details");
        Console.WriteLine("3. List all appointments");
        Console.WriteLine("4. Book appointment");
        Console.WriteLine("5. Exit to login");
        Console.WriteLine("6. Exit system");

        int optionInt;
        Boolean validInput;
        do
        {
            string option = Console.ReadLine();
            optionInt = int.Parse(option);
            validInput = (optionInt <= 6 && optionInt >= 1);
            if (!validInput)
            {
                Console.WriteLine("Invalid Input, Please enter a number between 1 and 6");
            }
        } while (!validInput);
            
        switch (optionInt)
        {
            case 1:
                DisplayPatientDetails();
                break;

            case 2:
                DisplayDoctorDetails();
                break;

            case 3:
                ListAppointments();
                break;
            case 4:
                BookAppointment();
                break;
            case 5:
                MainMenu.DisplayMainMenu();
                break;

            case 6:
                _patients = null;
                _doctors = null;
                _appointments = null;
                GC.Collect();
                Environment.Exit(1);
                break;

            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    public void DisplayPatientDetails()
    {
        Console.Clear();
        Utils.PageHeader("My Details");
        Console.WriteLine($"{_patient.Name}'s Details");
        Console.WriteLine("");
        Console.WriteLine($"Patient ID {_patient.Id}");
        Console.WriteLine($"Full Name {_patient.Name}");
        Console.WriteLine($"Address: {_patient.Address}");
        Console.WriteLine($"Email: {_patient.Email}");
        Console.WriteLine($"Phone Number: {_patient.PhoneNumber}");
        Console.ReadKey();
        DisplayPatientMenu();
    }

    public void DisplayDoctorDetails()
    {
        Console.Clear();
        Utils.PageHeader("My Doctor");

        if (_patient.Doctor != null)
        {
            Utils.DoctorHeader();
            _patient.Doctor.DisplayDetails();
        }
        else
        {
            Console.WriteLine("You dont currently have an assigned doctor");
        }
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the patient menu");
        Console.ReadKey();
        DisplayPatientMenu();
        
    }

    private void ListAppointments()
    {
        Console.Clear();
        Utils.PageHeader("My Appointments");
        Console.WriteLine();
        Console.WriteLine($"Appointments for {_patient.Name}");
        Console.WriteLine();
        Utils.AppointmentHeader();
        List<Appointment> patientAppointments = _appointments
            .Where(appointment => appointment.Patient.Id == _patient.Id)
            .ToList();
        foreach (var appointment in patientAppointments)
        {
            appointment.DisplayDetails();
        }
        
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the patient menu");
        Console.ReadKey();
        DisplayPatientMenu();
    }

    private void BookAppointment()
    {
        Console.Clear();
        Utils.PageHeader("Book Appointment");
        if (_patient.Doctor == null)
        {
            Console.WriteLine("You are not registered with a doctor");
            Console.WriteLine("Please choose a doctor you would like to register with:");
            var index = 0;
            foreach (var doctor in _doctors)
            {
                index++;
                Console.Write($"{index} ");
                doctor.DisplayDetails();
            }

            string selectedDoctor = Console.ReadLine();
            _patient.Doctor = _doctors[int.Parse(selectedDoctor) - 1];
            FileManager.SavePatients(_patients);
            Console.WriteLine($"You have successfully selected your doctor");
            
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the patient menu");
        }
        
        Console.WriteLine($"You are booking a new appointment with {_patient.Doctor.Name}");
        Console.Write("Description of appointment: ");
        string description;
        do
        {
            description = Console.ReadLine();
            if (description == null)
            {
                Console.WriteLine("Please enter a valid description");
            }
        } while (description == null);

        Appointment appointment = new Appointment(_patient.Doctor, _patient, description);
        _appointments.Add(appointment);
        FileManager.SaveAppointments(_appointments);
        Console.WriteLine("The appointment has been successfully booked");
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the patient menu");
        Console.ReadKey();
        DisplayPatientMenu();
        
    }
}