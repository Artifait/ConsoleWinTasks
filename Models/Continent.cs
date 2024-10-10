using System;
using System.Collections.Generic;

namespace ConsoleWinTasks;

public partial class Continent
{
    public int Id { get; set; }

    public string ContinentName { get; set; } = null!;

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
