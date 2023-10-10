using JedProject_2.DAL;
using JedProject_2.Models.Base;

namespace JedProject_2.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Product>? Products { get; set; }
    public Category()
    {
        Products = new HashSet<Product>();
    }

    public static void GetCategories()
    {
        AppDbContext context = new AppDbContext();
        var categories = context.Categories.ToList();
        if (categories.Any())
        {
            categories.ForEach(cat => Console.WriteLine($"{cat.Id} -- {cat.Name}"));
        }
        else Console.WriteLine("Not found!");
    }

    public static bool CheckCategory(int categoryId)
    {
        AppDbContext context = new AppDbContext();
        var category = context.Categories.FirstOrDefault(x => x.Id == categoryId);
        return category != null;
    }

    public static void AddCategory(string name)
    {
        AppDbContext context = new AppDbContext();
        context.Categories.Add(new Category() { Name = name });
        context.SaveChanges();
        Console.WriteLine("Category Added!");
    }
}
