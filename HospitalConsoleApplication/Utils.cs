namespace HospitalConsoleApplication;

static class Utils
{
    public static int GenerateUserId()
    {
        Random random = new Random();
        int id = random.Next(1, 99999);
        
        //ToDo check if id is already in database
        
        return id;
    }

    public static void WriteSpaces(int spaces)
    {
        for (var i = 0; i < spaces; i++)
        {
            Console.Write(" ");
        }
    }

    public static void WriteDashes(int dashes)
    {
        for (var i = 0; i < dashes; i++)
        {
            Console.Write("-");
        }
    }

    public static void DoctorHeader()
    {
        Console.WriteLine("Your Doctor:");
        Console.Write("| Name");
        WriteSpaces(16);
        Console.Write("| ");
        Console.Write("Email");
        WriteSpaces(15);
        Console.Write("| ");
        Console.Write("Phone number");
        WriteSpaces(1);
        Console.Write("| ");
        Console.Write("Address");
        WriteSpaces(22);
        Console.WriteLine();
        Console.Write("|");
        WriteDashes(90);
        Console.WriteLine();
    }

    public static void PatientHeader()
    {
        Console.Write("| ");
        Console.Write("Name");
        WriteSpaces(16);
        Console.Write("| ");
        Console.Write("Doctor");
        WriteSpaces(14);
        Console.Write("| ");
        Console.Write("Email");
        WriteSpaces(15);
        Console.Write("| ");
        Console.Write("Phone number");
        WriteSpaces(1);
        Console.Write("| ");
        Console.Write("Address");
        Console.WriteLine();
        WriteDashes(110);
        Console.WriteLine();
    }

    public static void PageHeader(string text)
    {
        int lengthBefore = (50-text.Length) / 2;
        int lengthAfter = 50 - text.Length - lengthBefore;
        Console.WriteLine("┌──────────────────────────────────────────────────┐");
        Console.WriteLine("│        DOTNET Hospital Management System         │");
        Console.WriteLine("├──────────────────────────────────────────────────┤");
        Console.Write("│");
        WriteSpaces(lengthBefore);
        Console.Write(text);
        WriteSpaces(lengthAfter);
        Console.WriteLine("│");
        Console.WriteLine("└──────────────────────────────────────────────────┘");
    }
}