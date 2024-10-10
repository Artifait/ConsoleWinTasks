using System;
using System.Collections.Generic;

namespace ConsoleWinTasks;

public partial class City
{
    public int Id { get; set; }

    public string CityName { get; set; } = null!;

    public int CityPopulation { get; set; }

    public int IdCountry { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

    public virtual Country IdCountryNavigation { get; set; } = null!;
}
