namespace HospitalConsoleApplication;

public class Administrator
{
    public int Id { get; }
    public string password { get; }

    public Administrator(int id, string password)
    {
        Id = id;
        this.password = password;
    }
}