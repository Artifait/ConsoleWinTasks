
namespace ConsoleWinTasks;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public override string ToString() => $"The book {Book} is reserved for {User}.";
}
