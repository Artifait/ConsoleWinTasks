
namespace ConsoleWinTasks;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public ICollection<Sale> Shopping { get; set; } = new List<Sale>();
    public override string ToString() => Username;
}
