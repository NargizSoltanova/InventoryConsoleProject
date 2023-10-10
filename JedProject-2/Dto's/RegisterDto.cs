using System.ComponentModel.DataAnnotations;

namespace JedProject_2.Dto_s;

public class RegisterDto
{
    public int Role { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
