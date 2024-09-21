namespace HospitalConsoleApplication;

public abstract class BaseUser
{
    public int Id { get; }
    public string Password { get; }

    public BaseUser(int id, string password)
    {
        Id = id;
        this.Password = password;
    }

    public BaseUser(string password)
    {
        this.Password = password;
        Id = Utils.GenerateUserId();
    }
    
    public abstract string ToCSVString();
}