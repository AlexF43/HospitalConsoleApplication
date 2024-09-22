namespace HospitalConsoleApplication;

public class UserValidation
{
    // deligate definition 
    public delegate bool ValidationRule(string input);

    // deligate method, uses the rule to validate the string, if it failes then print the error message
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