namespace HospitalConsoleApplication;

public class DoctorMenu
{
    private Doctor _doctor;
    private List<Patient> _patients;
    private List<Appointment> _appointments;

    public DoctorMenu(Doctor doctor, List<Patient> patients, List<Appointment> appointments)
    {
        this._doctor = doctor;
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
        _doctor.DisplayDetails();
        Console.ReadLine();
        DisplayDoctorMenu();
    }

    private void DisplayListOfPatients()
    {
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

        Console.ReadLine();
        DisplayDoctorMenu();
    }
    
    private void ListAppointments()
    {
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

        Console.ReadLine();
        DisplayDoctorMenu();
    }

    private void SearchPatientDetails()
    {
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
        Console.ReadLine();
        DisplayDoctorMenu();
    }

    private void ListAppointmentsWithPatient()
    {
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

        Console.ReadLine();
        DisplayDoctorMenu();
    }
}