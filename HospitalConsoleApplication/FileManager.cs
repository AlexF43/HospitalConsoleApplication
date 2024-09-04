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
    
    
}