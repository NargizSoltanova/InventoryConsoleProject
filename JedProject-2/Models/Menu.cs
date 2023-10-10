namespace JedProject_2.Models;

public class Menu
{
    public static void GetMenu()
    {
        Console.WriteLine("====Welcome====");
        Console.WriteLine("1 - Register");
        Console.WriteLine("2 - Login");
        Console.WriteLine("0 - Quit");
    }

    public enum MainMenu
    {
        Register = 1,
        Login = 2
    }


    public static void AdminMenu()
    {
        Console.WriteLine("=================");
        Console.WriteLine("1 - Products");
        Console.WriteLine("2 - Purveyors");
        Console.WriteLine("3 - Sales");
        Console.WriteLine("4 - Reports");
        Console.WriteLine("5 - Search");
        Console.WriteLine("0 - Quit");
        Console.WriteLine("=================");
    }

    public enum MenuAdmin
    {
        Products = 1,
        Purveyors,
        Sales,
        Reports,
        Search
    }


    public static void SearchMenu()
    {
        Console.WriteLine("1 - Search Products by name");
        Console.WriteLine("2 - Search Products by category");
        Console.WriteLine("3 - Search Purveyors");
        Console.WriteLine("4 - Search Users by email or username");
        Console.WriteLine("5 - Search Sales with date");
        Console.WriteLine("0 - Quit");
    }

    public enum MenuSearch
    {
        SearchProductName = 1,
        SearchProductCategory,
        SearchPurveyors,
        SearchUsers,
        SearchSales,
    }

    public static void ReportMenu()
    {
        Console.WriteLine("1 - Report for categories");
        Console.WriteLine("2 - Report for sales");
        Console.WriteLine("0 - Quit");
    }

    public enum MenuReport
    {
        CategoryReport = 1,
        SaleReport
    }

    public static bool CheckPassword(string password)
    {
        int integer = 0;
        int upper = 0;
        int lower = 0;
        foreach (var item in password)
        {
            if (Char.IsDigit(item)) integer++;
            if (Char.IsLower(item)) lower++;
            if (Char.IsUpper(item)) upper++;
        }
        if (integer != 0 && upper != 0 && lower != 0 && password.Length > 5) return true;
        else
        {
            Console.WriteLine("The password must contain whole numbers, uppercase and lowercase letters. The minimum password length must be 6");
            return false;
        }
    }

    public static bool CheckEmail(string email)
    {
        if (!email.Contains("@") || email.Length < 7)
        {
            Console.WriteLine("Enter a valid email type");
            return false;
        }
        return true;
    }

    public static bool CheckLength(string word)
    {
        if (word.Length < 6)
        {
            Console.WriteLine("Minimun length must be 6");
            return false;
        }
        return true;
    }
}

