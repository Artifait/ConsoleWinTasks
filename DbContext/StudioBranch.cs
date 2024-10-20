
namespace ConsoleWinTasks;

public class StudioBranch
{
    public int Id { get; set; }
    public string CityName { get; set; }

    // Связь с студией
    public int StudioId { get; set; }
    public Studio Studio { get; set; }
}