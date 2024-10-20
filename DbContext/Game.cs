
namespace ConsoleWinTasks;

public partial class Game
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? StuleGame { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? GameMode { get; set; }
    public int CopiesSold { get; set; }

    // Связь с студией
    public int StudioId { get; set; }
    public Studio Studio { get; set; }
}
