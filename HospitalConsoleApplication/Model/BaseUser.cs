namespace HospitalConsoleApplication;

// basic class that all users inherit from
public abstract class BaseUser
{
    public int Id { get; }
    public string Password { get; }

    // initialisers with the id for when being created from the file manager
    public BaseUser(int id, string password)
    {
        Id = id;
        this.Password = password;
    }

    // initialisation without id for creating users manually with randomly assigned id's
    public BaseUser(string password)
    {
        this.Password = password;
        Id = Utils.GenerateUserId();
    }
    
    // method used to store users information in a csv type file
    public abstract string ToCSVString();
}