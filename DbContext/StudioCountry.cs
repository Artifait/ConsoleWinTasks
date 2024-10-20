
namespace ConsoleWinTasks;

public class StudioCountry
{
    public int Id { get; set; }
    public string CountryName { get; set; }

    // Связь с студией
    public int StudioId { get; set; }
    public Studio Studio { get; set; }
}