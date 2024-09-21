namespace HospitalConsoleApplication;

public class Administrator : BaseUser
{
    public Administrator(int id, string password) : base(id, password)
    {
    }
    
    public override string ToCSVString()
    {
        string csvString = Id + ", " + Password;
        return csvString;
    } 
}