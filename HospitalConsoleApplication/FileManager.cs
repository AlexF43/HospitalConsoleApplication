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
                    string[] parts = currentLine.Split(',');
                    
                    
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
                    string[] parts = currentLine.Split(',');
                    Doctor? doctor = doctors.Find(x => x.Id == int.Parse(parts[6]));
                    patients.Add(new Patient(int.Parse(parts[0]), parts[1],
                        parts[2], parts[3], parts[4],
                        int.Parse(parts[5]), doctor));
                }
            }
        }
        return patients;
    }
    
    
}