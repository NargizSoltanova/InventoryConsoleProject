using JedProject_2.DAL;
using JedProject_2.Models.Base;

namespace JedProject_2.Models;

public class Storage : BaseEntity
{
    public string Name { get; set; }
    public Company? Company { get; set; }
    public int CompanyId { get; set; }
    public ICollection<PurveyorStorage>? PurveyorStorages { get; set; }
    public ICollection<Product>? Products { get; set; }
    public Storage()
    {
        PurveyorStorages = new HashSet<PurveyorStorage>();
        Products = new HashSet<Product>();
    }

    public static int GetStorages(int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        if (storages.Any())
        {
            storages.ForEach(sto => { Console.WriteLine($"{sto.Id} -- {sto.Name}"); });
            return storages.Count();
        }
        else
        {
            Console.WriteLine("Not Found!"); return 0;
        }
    }

    public static bool CheckStorage(int id)
    {
        AppDbContext context = new AppDbContext();
        var storage = context.Storages.FirstOrDefault(x => x.Id == id);
        if (storage != null) return true;
        else return false;
    }

    public static void AddStorage(string name, int companyId)
    {
        AppDbContext context = new AppDbContext();
        context.Storages.Add(new Storage() { Name = name, CompanyId = companyId });
        context.SaveChanges();
        Console.WriteLine("Storage Added!");
    }
}
