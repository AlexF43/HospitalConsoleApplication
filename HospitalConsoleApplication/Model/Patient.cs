namespace HospitalConsoleApplication;

public class Patient : User
{
    private Doctor? Doctor { get; set; }

    public Patient(string name, string password, string email, string address, int phoneNumber) : base(name, password, email, address, phoneNumber)
    {
    }

    public Patient(int id, string name, string password, string email, string address, int phoneNumber, Doctor? doctor) : base(id, name, password, email, address, phoneNumber)
    {
        Doctor = doctor;
    }
    

    public override void DisplayDetails()
    {
        Console.Write(Name);
        Utils.WriteSpaces(20-Name.Length);
        Console.Write("|");
        Console.Write(Doctor);
        Utils.WriteSpaces(20-Doctor.Name.Length);
        Console.Write("|");
        Console.Write(Email);
        Utils.WriteSpaces(20-Email.Length);
        Console.Write("|");
        Console.Write(PhoneNumber);
        Utils.WriteSpaces(10-PhoneNumber.ToString().Length);
        Console.Write("|");
        Console.Write(Address);
        Utils.WriteSpaces(30-Address.Length);
    }

    public override string ToCSVString()
    {
        string csvString = Id + ", " + Name + ", " + Password + ", " + Email
            + ", " + Address + ", " + PhoneNumber + ", " + Doctor.Id;  
        return csvString;
    } 
}