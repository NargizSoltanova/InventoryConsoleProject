using JedProject_2.DAL;
using JedProject_2.Models.Base;

namespace JedProject_2.Models;

public class PurveyorStorage : BaseEntity
{
    public Purveyor? Purveyor { get; set; }
    public Storage? Storage { get; set; }
    public int PurveyorId { get; set; }
    public int StorageId { get; set; }

    public static void Get()
    {
        AppDbContext context = new AppDbContext();
        var list = context.PurveyorStorages.ToList();
        if (list.Any())
        {
            list.ForEach(x => { Console.WriteLine(); });
        }
        else Console.WriteLine("Not Found!");
    }

    public static void AddPurSto(int purveyorId, int storageId)
    {
        AppDbContext context = new AppDbContext();
        context.PurveyorStorages.Add(new PurveyorStorage { PurveyorId = purveyorId, StorageId = storageId });
        context.SaveChanges();
        Console.WriteLine("Added!");
    }
}
