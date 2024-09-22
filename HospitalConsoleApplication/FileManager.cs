namespace HospitalConsoleApplication;

static class FileManager
{
    private const string PATIENTS_FILE = "patients.txt";
    private const string DOCTORS_FILE = "doctors.txt";
    private const string ADMINS_FILE = "admins.txt";
    private const string APPOINTMENTS_FILE = "appointments.txt";
    
    public static void SavePatients(List<Patient> patients)
    {
        using (StreamWriter writer = new StreamWriter(PATIENTS_FILE))
        {
            foreach (var patient in patients)
            {
                writer.WriteLine(patient.ToCSVString());
            }
        }
    }
    
    public static void SaveDoctors(List<Doctor> doctors)
    {
        using (StreamWriter writer = new StreamWriter(DOCTORS_FILE))
        {
            foreach (var doctor in doctors)
            {
                writer.WriteLine(doctor.ToCSVString());
            }
        }
    }
    
    public static List<Doctor> LoadDoctors()
    {
        List<Doctor> patients = new List<Doctor>();
        if (File.Exists(DOCTORS_FILE))
        {
            using (StreamReader reader = new StreamReader(DOCTORS_FILE))
            {
                while (reader.ReadLine() is { } currentLine)
                {
                    string[] parts = currentLine.Split(", ");
                    
                    
                    patients.Add(new Doctor(int.Parse(parts[0]), parts[1],
                        parts[2], parts[3], parts[4],
                        int.Parse(parts[5])));
                }
            }
        }
        return patients;
    }
    
    public static List<Patient> LoadPatients(List<Doctor> doctors)
    {
        List<Patient> patients = new List<Patient>();
        if (File.Exists(PATIENTS_FILE))
        {
            using (StreamReader reader = new StreamReader(PATIENTS_FILE))
            {
                while (reader.ReadLine() is { } currentLine)
                {
                    string[] parts = currentLine.Split(", ");
                    Doctor? doctor = null;
                    if (parts.Length > 6 && !string.IsNullOrEmpty(parts[6]))
                    {
                        int doctorId;
                        if (int.TryParse(parts[6], out doctorId))
                        {
                            doctor = doctors.Find(x => x.Id == doctorId);
                        }
                    }
                    patients.Add(new Patient(int.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], int.Parse(parts[5]), 
                        doctor
                    ));
                }
            }
        }
        return patients;
    }
    
    public static void SaveAdministrators(List<Administrator> administrators)
    {
        using (StreamWriter writer = new StreamWriter(ADMINS_FILE))
        {
            foreach (var admin in administrators)
            {
                writer.WriteLine(admin.ToCSVString());
            }
        }
    }

    public static List<Administrator> LoadAdministrators()
    {
        List<Administrator> administrators = new List<Administrator>();
        if (File.Exists(ADMINS_FILE))
        {
            using (StreamReader reader = new StreamReader(ADMINS_FILE))
            {
                while (reader.ReadLine() is { } currentLine)
                {
                    string[] parts = currentLine.Split(", ");
                    administrators.Add(new Administrator(int.Parse(parts[0]), parts[1]));
                }
            }
        }
        return administrators;
    }

    public static void SaveAppointments(List<Appointment> appointments)
    {
        using (StreamWriter writer = new StreamWriter(APPOINTMENTS_FILE))
        {
            foreach (var appointment in appointments)
            {
                writer.WriteLine(appointment.ToCSVString());
            }
        }
    }

    public static List<Appointment> LoadAppointments(List<Doctor> doctors, List<Patient> patients)
    {
        List<Appointment> appointments = new List<Appointment>();
        if (File.Exists(APPOINTMENTS_FILE))
        {
            using (StreamReader reader = new StreamReader(APPOINTMENTS_FILE))
            {
                while (reader.ReadLine() is { } currentLine)
                {
                    string[] parts = currentLine.Split(", ");
                    Doctor? doctor = doctors.Find(x => x.Id == int.Parse(parts[0]));
                    Patient? patient = patients.Find(x => x.Id == int.Parse(parts[1]));
                    if (doctor != null && patient != null)
                    {
                        appointments.Add(new Appointment(doctor, patient, parts[2]));
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Could not load appointment, missing doctor or patient. Doctor ID: {parts[0]}, Patient ID: {parts[1]}");
                    }
                }
            }
        }
        return appointments;
    }
}