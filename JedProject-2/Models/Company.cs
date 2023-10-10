using JedProject_2.DAL;
using JedProject_2.Models.Base;

namespace JedProject_2.Models;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Storage>? Storages { get; set; }
    public Company()
    {
        Storages = new HashSet<Storage>();
    }

    public static void GetCompanies()
    {
        AppDbContext context = new AppDbContext();
        var companies = context.Companies.ToList();
        if (companies.Any())
        {
            companies.ForEach(com => { Console.WriteLine($"{com.Name} -- {com.Address}"); });
        }
        else Console.WriteLine("Not Found!");
    }

    public static (bool check, int id) CheckCompany(string name)
    {
        if (name.Trim() == "") return (false, 0);
        AppDbContext context = new AppDbContext();
        var company = context.Companies.FirstOrDefault(com => com.Name.ToLower() == name.ToLower());
        return (company != null, (company != null ? company.Id : 0));
    }

    public static void AddCompany(string name, string address)
    {
        AppDbContext context = new AppDbContext();
        context.Companies.Add(new Company() { Name = name, Address = address });
        context.SaveChanges();
        Console.WriteLine("Company Added!");
    }
}
