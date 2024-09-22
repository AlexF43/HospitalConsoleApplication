namespace HospitalConsoleApplication;

// doctor class inherits from user
public class Doctor : User
{

    // for manually creating a new doctor
    public Doctor(string name, string password, string email, string address, int phoneNumber) :
        base(name, password, email, address, phoneNumber)
    {
    }

    // initialiser for initialising from a file
    public Doctor(int id, string name, string password, string email, string address, int phoneNumber) : 
        base(id, name, password, email, address, phoneNumber)
    {
    }

    // displays the doctors infomation in a table row like format
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
        Console.WriteLine();
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
    
    // converts the doctor object variables into a csv type string for storage
    public override string ToCSVString()
    {
        string csvString = Id + ", " + Name + ", " + Password + ", " + Email
                           + ", " + Address + ", " + PhoneNumber;
        return csvString;
    } 
}