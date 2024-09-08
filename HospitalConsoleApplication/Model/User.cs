namespace HospitalConsoleApplication;

public abstract class User
{
    public int Id { get; }
    public string Name { get; }
    public string Password { get; }
    public string Email { get; }
    public string Address { get; }
    public int PhoneNumber { get; }


    protected User(string name, string password, string email, string address, int phoneNumber)
    {
        Id = Utils.GenerateUserId();
        Name = name;
        Password = password;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    protected User(int id, string name, string password, string email, string address, int phoneNumber)
    {
        Id = id;
        Name = name;
        Password = password;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    public abstract void DisplayDetails();

    public abstract string ToCSVString();
}