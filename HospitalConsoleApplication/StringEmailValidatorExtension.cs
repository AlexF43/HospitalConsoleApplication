namespace HospitalConsoleApplication;

public static class StringEmailValidatorExtension
{
    public static bool IsValidEmail(this string email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
    }
}