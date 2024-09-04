namespace HospitalConsoleApplication;

public class Patient : User
{

    public string Email { get; }
    public string Address { get; }
    public int PhoneNumber { get; }
    public Doctor? Doctor { get; set; }

    public Patient(string name, string password, string email,
        string address, int phoneNumber, Doctor doctor) : base(name, password)
    {
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
        Doctor = doctor;
    }
    
    public Patient(int id, string name, string password, string email,
        string address, int phoneNumber, Doctor? doctor) : base(id, name, password)
    {
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
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
            + ", " + Address + ", " + PhoneNumber + ", " + PhoneNumber + ", "
            + Doctor.Id;  
        return csvString;
    } 
}