using JedProject_2.DAL;
using JedProject_2.Dto_s;
using JedProject_2.Models.Base;

namespace JedProject_2.Models;

public class User : BaseEntity
{
    public int Role { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public ICollection<Sale>? Sales { get; set; }
    public User()
    {
        Sales = new HashSet<Sale>();
    }

    public static bool CheckRole(int roleId)
    {
        bool result = false;
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            if ((int)role == roleId) result = true;
        }
        return result;
    }

    public static void Search(string text)
    {
        AppDbContext context = new AppDbContext();
        if (text.Contains("@"))
        {
            var user = context.Users.FirstOrDefault(x => x.Email == text);
            if (user == null)
            {
                Console.WriteLine("User not found by email!"); return;
            }
            else Console.WriteLine($"Fullname: {user.Fullname}  Username: {user.Username}  Email: {user.Email}");
        }
        else
        {
            var users = context.Users.Where(x => x.Username.ToLower() == text.ToLower()).ToList();
            if (users.Any())
            {
                users.ForEach(user => { Console.WriteLine($"Fullname: {user.Fullname}  Username: {user.Username}  Email: {user.Email}"); });
            }
            else Console.WriteLine("User not found by username!");
        }
    }

    public static void AddUser(RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            Console.WriteLine("Password and Confirm Password not equal!");
            return;
        }
        AppDbContext context = new AppDbContext();
        context.Users.Add(new User()
        {
            Role = registerDto.Role,
            Fullname = registerDto.Fullname,
            Username = registerDto.Username,
            Email = registerDto.Email,
            Password = registerDto.Password,
            ConfirmPassword = registerDto.ConfirmPassword
        });
        try
        {
            context.SaveChanges();
        }
        catch (Exception)
        {
            Console.WriteLine("Duplicate email!");
            return;
        }
        Console.WriteLine("Added");
    }

    public static (bool check, User user) Login(LoginDto login)
    {
        AppDbContext context = new AppDbContext();
        var user = context.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
        if (user == null)
        {
            Console.WriteLine("Invalid Credentails!");
            return (false, user);
        }
        else
        {
            Console.WriteLine($"Welcome {user.Username}");
            return (true, user);
        }
    }

    public static void GetRoles()
    {
        int count = 1;
        foreach (var item in Enum.GetNames(typeof(Roles)))
        {
            Console.WriteLine($"{count} - {item}");
            count++;
        }
    }

    public enum Roles
    {
        Admin = 1,
        Member = 2
    }
}
