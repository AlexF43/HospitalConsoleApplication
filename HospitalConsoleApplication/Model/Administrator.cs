namespace HospitalConsoleApplication;

// administrator class inherits from base user
public class Administrator : BaseUser
{
    public Administrator(int id, string password) : base(id, password)
    {
    }
    
    // implementation of csvstring method to store an admins information ina file
    public override string ToCSVString()
    {
        string csvString = Id + ", " + Password;
        return csvString;
    } 
}