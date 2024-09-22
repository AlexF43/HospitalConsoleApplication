namespace HospitalConsoleApplication;

// patient object, inhertis from user
public class Patient : User
{
    public Doctor? Doctor { get; set; }

    // initialisers for manually creating, and for initialising from a file, with and without an assigned doctor
    public Patient(string name, string password, string email, string address, int phoneNumber) : base(name, password, email, address, phoneNumber)
    {
    }

    public Patient(int id, string name, string password, string email, string address, int phoneNumber) : base(id, name, password, email, address, phoneNumber)
    {
    }
    
    public Patient(int id, string name, string password, string email, string address, int phoneNumber, Doctor? doctor) : base(id, name, password, email, address, phoneNumber)
    {
        Doctor = doctor;
    }
    
    // displays the patient data in a table row format
    public override void DisplayDetails()
    {
        Console.Write("| ");
        Console.Write(Name);
        Utils.WriteSpaces(20 - Name.Length);
        Console.Write("| ");
        string doctorName = Doctor?.Name ?? "No Doctor";
        Console.Write(doctorName);
        Utils.WriteSpaces(20 - doctorName.Length);
        Console.Write("| ");
        Console.Write(Email);
        Utils.WriteSpaces(20 - Email.Length);
        Console.Write("| ");
        Console.Write(PhoneNumber);
        Utils.WriteSpaces(13 - PhoneNumber.ToString().Length);
        Console.Write("| ");
        Console.Write(Address);
        Utils.WriteSpaces(30 - Address.Length);
        Console.WriteLine();
    }

    // converts the patient obeject into a csv type format for file storage
    public override string ToCSVString()
    {
        string csvString;
        if (Doctor != null)
        {
            csvString = Id + ", " + Name + ", " + Password + ", " + Email
                               + ", " + Address + ", " + PhoneNumber + ", " + Doctor.Id;  

        }
        else
        {
            csvString = Id + ", " + Name + ", " + Password + ", " + Email
                               + ", " + Address + ", " + PhoneNumber;
        }
        return csvString;
    } 
}