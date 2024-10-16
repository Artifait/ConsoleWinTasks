using System;
using System.Collections.Generic;

namespace ConsoleWinTasks;

public partial class Game
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? StuleGame { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
