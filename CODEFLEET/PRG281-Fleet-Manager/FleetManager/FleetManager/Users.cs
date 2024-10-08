using System.Dynamic;

public class User
{
    public required int UserNo { get; set; }
    public required string UserID { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required int Role { get; set; }
    public required string Password { get; set; }
    public required bool Active { get; set; }
    public string DisplayFullname()
    {
        return Name + ' ' + Surname;
    }
}

