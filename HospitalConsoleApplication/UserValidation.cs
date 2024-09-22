namespace HospitalConsoleApplication;

public class UserValidation
{
    public delegate bool ValidationRule(string input);

    public static bool ValidateInput(string input, ValidationRule rule, string errorMessage)
    {
        if (rule(input))
        {
            return true;
        }
        else
        {
            Console.WriteLine(errorMessage);
            return false;
        }
    }
}