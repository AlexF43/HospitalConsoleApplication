namespace HospitalConsoleApplication;

// utils class holding many useful method to be used througout the project
static class Utils
{
    
    // function to print a specified number of spaces in a row
    public static void WriteSpaces(int spaces)
    {
        for (var i = 0; i < spaces; i++)
        {
            Console.Write(" ");
        }
    }

    // function to print a specified number of dashes
    public static void WriteDashes(int dashes)
    {
        for (var i = 0; i < dashes; i++)
        {
            Console.Write("-");
        }
    }
    
    // function to generate id's
    public static int GenerateUserId()
    {
        Random random = new Random();
        int id = random.Next(1000, 99999);
        return id;
    }

    // header for a table displaying doctor infomation
    public static void DoctorHeader()
    {
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

    // header for a table displaying patient infomation
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

    // header for a table displaying appointment information
    public static void AppointmentHeader()
    {
        Console.Write("Doctor");
        WriteSpaces(24);
        Console.Write("| ");
        Console.Write("Patient");
        WriteSpaces(23);
        Console.Write("| ");
        Console.Write("Description");
        WriteSpaces(29);
        Console.WriteLine();
        WriteDashes(100);
        Console.WriteLine();
    }

    // header for each screen
    // takes in a string and will print the string inside the hospital management header 
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