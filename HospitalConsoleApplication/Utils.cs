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
}