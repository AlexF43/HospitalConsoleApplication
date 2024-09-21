namespace HospitalConsoleApplication;

public class Appointment
{

    public Doctor Doctor { get; }
    public Patient Patient { get; }
    public string Description { get; }
    
    public Appointment(Doctor doctor, Patient patient, string description)
    {
        Doctor = doctor;
        Patient = patient;
        Description = description;
    }

    public string ToCSVString()
    {
        string csvString = Doctor.Id + ", " + Patient.Id + ", " + Description;
        return csvString;
    }
}

