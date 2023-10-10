using JedProject_2.DAL;
using JedProject_2.Models.Base;

namespace JedProject_2.Models;

public class Purveyor : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Product>? Products { get; set; }
    public ICollection<PurveyorStorage>? PurveyorStorages { get; set; }
    public Purveyor()
    {
        PurveyorStorages = new HashSet<PurveyorStorage>();
        Products = new HashSet<Product>();
    }

    public static void PurveyorMenu()
    {
        Console.WriteLine("1 - Add Purveyor");
        Console.WriteLine("2 - Update Purveyor");
        Console.WriteLine("3 - Delete Purveyor");
    }

    public enum MenuPurveyor
    {
        AddPurveyor = 1,
        UpdatePurveyor,
        DeletePurveyor
    }

    public static bool ExistPurveyor(string name)
    {
        AppDbContext context = new AppDbContext();
        var purveyor = context.Purveyors.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        if (purveyor != null)
        {
            Console.WriteLine("Purveyor alredy exists!");
            return true;
        }
        return false;
    }

    public static void GetPurveyors()
    {
        AppDbContext context = new AppDbContext();
        var purveyors = context.Purveyors.ToList();
        if (purveyors.Any())
        {
            purveyors.ForEach(pur => Console.WriteLine($"{pur.Id} -- {pur.Name}"));
        }
        else Console.WriteLine("Not found!");
    }

    public static void SearchByName(string name)
    {
        AppDbContext context = new AppDbContext();
        var purveyor = context.Purveyors.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        if (purveyor == null)
        {
            Console.WriteLine("Purveyor not found!"); return;
        }
        Console.WriteLine($"Name: {purveyor.Name}");
    }

    public static bool CheckPurveyor(int id)
    {
        AppDbContext context = new AppDbContext();
        var purveyor = context.Purveyors.FirstOrDefault(pur => pur.Id == id);
        if (purveyor != null) return true;
        else return false;
    }

    public static void AddPurveyor(string name)
    {
        AppDbContext context = new AppDbContext();
        context.Purveyors.Add(new Purveyor() { Name = name });
        context.SaveChanges();
        Console.WriteLine("Purveyor Added!");
    }

    public static void UpdateName(int purveyorId, string name)
    {
        AppDbContext context = new AppDbContext();
        var purveyor = context.Purveyors.FirstOrDefault(x => x.Id == purveyorId);
        if (purveyor == null)
        {
            Console.WriteLine("Purveyor not found!"); return;
        }
        purveyor.Name = name;
        context.Purveyors.Update(purveyor);
        context.SaveChanges();
        Console.WriteLine("Purveyor Updated!");
    }

    public static void DeletePurveyor(int id)
    {
        AppDbContext context = new AppDbContext();
        var purveyor = context.Purveyors.FirstOrDefault(x => x.Id == id);
        if (purveyor == null)
        {
            Console.WriteLine("Purveyor not found!"); return;
        }
        context.Purveyors.Remove(purveyor);
        context.SaveChanges();
        Console.WriteLine("Purveyor Deleted!");
    }
}
