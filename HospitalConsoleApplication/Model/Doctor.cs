namespace HospitalConsoleApplication;

public class Doctor : User
{

    public Doctor(string name, string password, string email, string address, int phoneNumber) :
        base(name, password, email, address, phoneNumber)
    {
    }

    public Doctor(int id, string name, string password, string email, string address, int phoneNumber) : 
        base(id, name, password, email, address, phoneNumber)
    {
    }

    public override void DisplayDetails()
    {
        Console.Write("| ");
        Console.Write(Name);
        Utils.WriteSpaces(20-Name.Length);
        Console.Write("| ");
        Console.Write(Email);
        Utils.WriteSpaces(20-Email.Length);
        Console.Write("| ");
        Console.Write(PhoneNumber);
        Utils.WriteSpaces(13-PhoneNumber.ToString().Length);
        Console.Write("| ");
        Console.Write(Address);
        Utils.WriteSpaces(30-Address.Length);
    }

    public void ListDetails()
    {
        Console.Write(Name);
        Console.Write(" | ");
        Console.Write(Email);
        Console.Write(" | ");
        Console.Write(PhoneNumber);
        Console.Write(" | ");
        Console.Write(Address);
    }
    
    public override string ToCSVString()
    {
        string csvString = Id + ", " + Name + ", " + Password + ", " + Email
                           + ", " + Address + ", " + PhoneNumber;
        return csvString;
    } 
}