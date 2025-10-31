namespace Projet_Finale.Utils;


public class DateTimeUtils
{
    public static DateTime ConvertToDateTime(String date)
    {
        if (DateTime.TryParse(date, out DateTime birthdate))
        {
            return DateTime.SpecifyKind(birthdate, DateTimeKind.Utc);
        }
        else
        {
            Console.WriteLine($"La date de P1 est mal renseignée");
            return DateTime.Now;
        }
    }
}
