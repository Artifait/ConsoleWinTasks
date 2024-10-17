using ConsoleWinApp.UI;
using ConsoleWinTasks;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinTasks.UI.Win.ApplicationWin;

public class Task4 : IWin
{
    #region DefaultPart
    public WindowDisplay windowDisplay = new("Task 4: Show TOPS", typeof(ProgramOptions));

    public WindowDisplay WindowDisplay
    {
        get => windowDisplay;
        set => windowDisplay = value;
    }

    public Type? ProgramOptionsType => typeof(ProgramOptions);
    public Type? ProgramFieldsType => null;

    public int SizeX => windowDisplay.MaxLeft;
    public int SizeY => windowDisplay.MaxTop;

    public void Show() => windowDisplay.Show();

    public void InputHandler()
    {
        char lower = char.ToLower(Console.ReadKey().KeyChar);
        WindowTools.UpdateCursorPos(lower, ref windowDisplay, (int)ProgramOptions.CountOptions);

        if (WindowTools.IsKeySelect(lower)) HandleMenuOption();
    }
    #endregion

    private void HandleMenuOption()
    {
        Console.Clear();
        PointDB db = Application.dB;

        Console.CursorTop = SizeY;
        switch ((ProgramOptions)windowDisplay.CursorPosition)
        {
            case ProgramOptions.ShowTop5CountriesByArea:
                ShowTop5CountriesByArea();
                break;

            case ProgramOptions.ShowTop5CapitalsByPopulation:
                ShowTop5CapitalsByPopulation();
                break;

            case ProgramOptions.ShowLargestCountryByArea:
                ShowLargestCountryByArea();
                break;

            case ProgramOptions.ShowLargestCapitalByPopulation:
                ShowLargestCapitalByPopulation();
                break;

            case ProgramOptions.ShowSmallestCountryInEuropeByArea:
                ShowSmallestCountryInEuropeByArea();
                break;

            case ProgramOptions.ShowAverageAreaOfEuropeanCountries:
                ShowAverageAreaOfEuropeanCountries();
                break;

            case ProgramOptions.ShowTop3CitiesByPopulationInCountry:
                ShowTop3CitiesByPopulationInCountry();
                break;

            case ProgramOptions.ShowTotalNumberOfCountries:
                ShowTotalNumberOfCountries();
                break;

            case ProgramOptions.ShowContinentWithMaxCountries:
                ShowContinentWithMaxCountries();
                break;

            case ProgramOptions.ShowNumberOfCountriesPerContinent:
                ShowNumberOfCountriesPerContinent();
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    public void ShowTop5CountriesByArea()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Countries
                        .OrderByDescending(c => c.Area)
                        .Take(5)
                        .Select(c => new { c.CountryName, c.Area });
            TV.DisplayTable(query);
        }
    }

    public void ShowTop5CapitalsByPopulation()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Cities
                        .OrderByDescending(c => c.CityPopulation)
                        .Take(5)
                        .Select(c => new { c.CityName, c.CityPopulation });
            TV.DisplayTable(query);
        }
    }

    public void ShowLargestCountryByArea()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Countries
                        .OrderByDescending(c => c.Area)
                        .FirstOrDefault();
            if (query != null)
            {
                TV.DisplayTable(new[] { new { query.CountryName, query.Area } });
            }
        }
    }

    public void ShowLargestCapitalByPopulation()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Cities
                        .OrderByDescending(c => c.CityPopulation)
                        .FirstOrDefault();
            if (query != null)
            {
                TV.DisplayTable(new[] { new { query.CityName, query.CityPopulation } });
            }
        }
    }

    public void ShowSmallestCountryInEuropeByArea()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Countries
                        .Where(c => c.IdContinentNavigation.ContinentName == "Europe")
                        .OrderBy(c => c.Area)
                        .FirstOrDefault();
            if (query != null)
            {
                TV.DisplayTable(new[] { new { query.CountryName, query.Area } });
            }
        }
    }

    public void ShowAverageAreaOfEuropeanCountries()
    {
        using (var context = new CountriesContext())
        {
            var averageArea = context.Countries
                        .Where(c => c.IdContinentNavigation.ContinentName == "Europe")
                        .Average(c => c.Area);
            TV.DisplayTable(new[] { new { AverageArea = averageArea } });
        }
    }

    public void ShowTop3CitiesByPopulationInCountry()
    {
        Console.WriteLine("Введите название страны:");
        string countryName = Console.ReadLine();

        using (var context = new CountriesContext())
        {
            var query = context.Cities
                        .Where(c => c.IdCountryNavigation.CountryName == countryName)
                        .OrderByDescending(c => c.CityPopulation)
                        .Take(3)
                        .Select(c => new { c.CityName, c.CityPopulation });

            TV.DisplayTable(query);
        }
    }

    public void ShowTotalNumberOfCountries()
    {
        using (var context = new CountriesContext())
        {
            var count = context.Countries.Count();
            TV.DisplayTable(new[] { new { TotalCountries = count } });
        }
    }

    public void ShowContinentWithMaxCountries()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Countries
                        .GroupBy(c => c.IdContinentNavigation.ContinentName)
                        .OrderByDescending(g => g.Count())
                        .Select(g => new { Continent = g.Key, CountryCount = g.Count() })
                        .FirstOrDefault();

            if (query != null)
            {
                TV.DisplayTable(new[] { query });
            }
        }
    }

    public void ShowNumberOfCountriesPerContinent()
    {
        using (var context = new CountriesContext())
        {
            var query = context.Countries
                        .GroupBy(c => c.IdContinentNavigation.ContinentName)
                        .Select(g => new { Continent = g.Key, CountryCount = g.Count() });

            TV.DisplayTable(query);
        }
    }

    public enum ProgramOptions
    {
        ShowTop5CountriesByArea,
        ShowTop5CapitalsByPopulation,
        ShowLargestCountryByArea,
        ShowLargestCapitalByPopulation,
        ShowSmallestCountryInEuropeByArea,
        ShowAverageAreaOfEuropeanCountries,
        ShowTop3CitiesByPopulationInCountry,
        ShowTotalNumberOfCountries,
        ShowContinentWithMaxCountries,
        ShowNumberOfCountriesPerContinent,
        Back,
        CountOptions
    }

}