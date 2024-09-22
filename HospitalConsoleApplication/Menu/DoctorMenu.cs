namespace HospitalConsoleApplication;

public class DoctorMenu
{
    private Doctor _doctor;
    private List<Patient> _patients;
    private List<Appointment> _appointments;

    public DoctorMenu(Doctor doctor, List<Patient> patients, List<Appointment> appointments)
    {
        _doctor = doctor;
        _patients = patients;
        _appointments = appointments;
    }

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

    private void SearchPatientDetails()
    {
        Console.Clear();
        Utils.PageHeader("Check Patient Details");
        Console.WriteLine();
        Console.Write("Enter the Id of the patient you would like to check: ");
        Patient? patient;
        do
        {
            var id = Console.ReadLine();
            patient = _patients.Find(p => id != null && p.Id == int.Parse(id));
            if (patient == null)
            {
                Console.WriteLine("Not a valid ID, Please try again");
            }
        } while (patient == null);
        
        Console.WriteLine();
        Utils.PatientHeader();
        patient.DisplayDetails();
        Console.WriteLine();
        Console.WriteLine("Press any key to return to the doctor menu");
        Console.ReadKey();
        DisplayDoctorMenu();
    }

    private void ListAppointmentsWithPatient()
    {
        Console.Clear();
        Utils.PageHeader("Appointments With");
        Console.WriteLine();
        Console.Write("Enter the Id of the patient you would like to view appointments for: ");
        Patient? patient;
        do
        {
            var id = Console.ReadLine();
            patient = _patients.Find(p => id != null && p.Id == int.Parse(id));
            if (patient == null)
            {
                Console.WriteLine("Not a valid ID, Please try again");
            }
        } while (patient == null);
        
        Console.WriteLine();
        Utils.AppointmentHeader();
        List<Appointment> patientAppointments = _appointments
            .Where(appointment => appointment.Patient.Id == patient.Id && appointment.Doctor.Id == _doctor.Id)
            .ToList();
        foreach (var appointment in patientAppointments)
        {
            appointment.DisplayDetails();
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the doctor menu");
        Console.ReadKey();
        DisplayDoctorMenu();
    }
}