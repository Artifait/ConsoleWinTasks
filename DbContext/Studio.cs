
namespace ConsoleWinTasks;

public class Studio
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Связь с городами, где находятся филиалы студии
    public ICollection<StudioBranch> StudioBranches { get; set; }

    // Связь с странами, где присутствует студия
    public ICollection<StudioCountry> StudioCountries { get; set; }

    // Связь с выпущенными играми
    public ICollection<Game> Games { get; set; }
}
