namespace HospitalConsoleApplication;

// appointment class associating a doctor with a patient
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

    // used to convert the appointment obeject into a csv type structure for storing in the file
    public string ToCSVString()
    {
        string csvString = Doctor.Id + ", " + Patient.Id + ", " + Description;
        return csvString;
    }

    // displays the appointment infomation as a row in a table
    public void DisplayDetails()
    {
        Console.Write(Doctor.Name);
        Utils.WriteSpaces(30-Doctor.Name.Length);
        Console.Write("| ");
        Console.Write(Patient.Name);
        Utils.WriteSpaces(30-Patient.Name.Length);
        Console.Write("| ");
        Console.Write(Description);
        Console.WriteLine();
    }
}

