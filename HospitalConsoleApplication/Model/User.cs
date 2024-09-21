namespace HospitalConsoleApplication;

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

    public abstract void DisplayDetails();
    
}