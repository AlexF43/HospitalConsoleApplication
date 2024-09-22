namespace HospitalConsoleApplication;

// user class inherits from base user and is the parent object of patients and doctors
public abstract class User : BaseUser
{
    public string Name { get; }
    public string Email { get; }
    public string Address { get; }
    public int PhoneNumber { get; }

    protected User(string name, string password, string email, string address, int phoneNumber) : base(password)
    {
        Name = name;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }
    
    protected User(int id, string name, string password, string email, string address, int phoneNumber) : base(id, password)
    {
        Name = name;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    // child classes (doctors, patients) must implement a method to display their data in a table row format
    public abstract void DisplayDetails();
    
}