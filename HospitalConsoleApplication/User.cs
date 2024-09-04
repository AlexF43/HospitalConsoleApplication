namespace HospitalConsoleApplication;

public abstract class User
{
    public int Id { get; }
    public string Name { get; }
    public string Password { get; }


    protected User(string name, string password)
    {
        Id = Utils.GenerateUserId();
        Name = name;
        Password = password;
    }
    
    protected User(int id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    public abstract void DisplayDetails();

    public abstract string ToCSVString();
}