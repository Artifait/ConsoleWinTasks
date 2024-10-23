
namespace ConsoleWinTasks;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();

    public override string ToString()
        => $"{LastName} {FirstName} {(string.IsNullOrWhiteSpace(MiddleName) ? "" : MiddleName + " ")}";
}
