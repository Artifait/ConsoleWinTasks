using System;
using System.Collections.Generic;

namespace ConsoleWinTasks;

public partial class Country
{
    public int Id { get; set; }

    public string CountryName { get; set; } = null!;

    public int Population { get; set; }

    public decimal Area { get; set; }

    public int IdContinent { get; set; }

    public string CapitalName { get; set; } = null!;

    public int? IdCapitalCity { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual City? IdCapitalCityNavigation { get; set; }

    public virtual Continent IdContinentNavigation { get; set; } = null!;
}
