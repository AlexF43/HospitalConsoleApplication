namespace HospitalConsoleApplication;

// class responsible for the patient visable menu
public class PatientMenu
{
    private Patient _patient;
    private List<Patient> _patients;
    private List<Appointment> _appointments;
    private List<Doctor> _doctors;

    // initialiser to populate all required fields
    public PatientMenu(Patient patient, List<Appointment> appointments, List<Doctor> doctors, List<Patient> patients)
    {
        _patient = patient;
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    // home screen of the patient menu
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
                validInput = (optionInt >= 1 && optionInt <= 6);
            }

            if (!validInput)
            {
                Console.WriteLine("Invalid Input, Please enter a number between 1 and 6");
            }
        } while (!validInput);
            
        // open the correct next screen depending on the user input
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
    
    // display the logged in users details
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

    // display the details of the patients doctor
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

    // display a list all of the patients appointments
    private void ListAppointments()
    {
        Console.Clear();
        Utils.PageHeader("My Appointments");
        Console.WriteLine();
        Console.WriteLine($"Appointments for {_patient.Name}");
        Console.WriteLine();

        List<Appointment> patientAppointments = _appointments
            .Where(appointment => appointment.Patient.Id == _patient.Id)
            .ToList();
        if (patientAppointments.Count > 0)
        {
            Utils.AppointmentHeader();
            foreach (var appointment in patientAppointments)
            {
                appointment.DisplayDetails();
            }
        }
        else
        {
            Console.WriteLine("You do not have any appointments booked");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the patient menu");
        Console.ReadKey();
        DisplayPatientMenu();
    }

    // screen where the patient can book appointments
    private void BookAppointment()
    {
        Console.Clear();
        Utils.PageHeader("Book Appointment");
        
        // if the patient does not have a doctor they are prompted to assign themselves one
        if (_patient.Doctor == null)
        {
            Console.WriteLine("You are not registered with a doctor");
            Console.WriteLine("Please choose a doctor you would like to register with:");
            var index = 0;
            // display all the doctors
            foreach (var doctor in _doctors)
            {
                index++;
                Console.Write($"{index} ");
                doctor.DisplayDetails();
            }

            int selectedDoctorIndex;
            bool validSelection = false;

            do
            {
                Console.Write("Enter the number of the doctor you want to select: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out selectedDoctorIndex))
                {
                    if (selectedDoctorIndex > 0 && selectedDoctorIndex <= _doctors.Count)
                    {
                        validSelection = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please enter a number within the range of available doctors.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (!validSelection);

            _patient.Doctor = _doctors[selectedDoctorIndex - 1];
            FileManager.SavePatients(_patients);
            Console.WriteLine($"You have successfully selected your doctor");
            
            Console.WriteLine();
        }
        
        // screen for booking an appointment
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