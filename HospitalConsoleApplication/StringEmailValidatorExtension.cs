namespace HospitalConsoleApplication;

// extension on the string class used to validate emails
public static class StringEmailValidatorExtension
{
    public static bool IsValidEmail(this string email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
    }
}