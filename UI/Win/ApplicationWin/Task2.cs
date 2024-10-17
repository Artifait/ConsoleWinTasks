using ConsoleWinApp.UI;
using ConsoleWinTasks;
using QuizTop;
using QuizTop.UI;

public class Task2 : IWin
{
    #region DefaultPart
    public WindowDisplay windowDisplay = new("Task 1: Show Data", typeof(ProgramOptions));

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
            case ProgramOptions.ShowAllCountry:
                ShowAllCountry();   
                break;

            case ProgramOptions.ShowCountryNames:
                ShowCountryNames();
                break;

            case ProgramOptions.ShowCapitalNames:
                ShowCapitalNames();
                break;

            case ProgramOptions.ShowCitiesForCountry:
                ShowCitiesForCountry();
                break;

            case ProgramOptions.ShowCapitalsWithPopulationOver5M:
                ShowCapitalsWithPopulationOver5M();
                break;

            case ProgramOptions.ShowEuropeanCountries:
                ShowEuropeanCountries();
                break;

            case ProgramOptions.ShowCountriesByArea:
                ShowCountriesByArea();
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    // 1. Отобразить всю информацию о странах
    public void ShowAllCountry()
    {
        using (var context = new CountriesContext())
        {
            var query = from country in context.Countries
                        join continent in context.Continents
                            on country.IdContinent equals continent.Id
                        join capitalCity in context.Cities
                            on country.IdCapitalCity equals capitalCity.Id into capitalJoin
                        from capital in capitalJoin.DefaultIfEmpty()
                        select new
                        {
                            country.CountryName,
                            country.CapitalName,
                            country.Population,
                            country.Area,
                            continent.ContinentName,
                            CapitalCityName = capital.CityName ?? "Столица отсутствует",
                            CapitalPopulation = capital != null ? capital.CityPopulation : 0
                        };

            TV.DisplayTable(query);
        }
    }

    // 2. Отобразить названия стран
    public void ShowCountryNames()
    {
        using (var context = new CountriesContext())
        {
            var query = from country in context.Countries
                        select new { country.CountryName };

            TV.DisplayTable(query);
        }
    }

    // 3. Отобразить названия столиц
    public void ShowCapitalNames()
    {
        using (var context = new CountriesContext())
        {
            var query = from country in context.Countries
                        select new { country.CapitalName };

            TV.DisplayTable(query);
        }
    }

    // 4. Отобразить названия крупных городов конкретной страны
    public void ShowCitiesForCountry()
    {
        Console.WriteLine("Введите название страны:");
        var countryName = Console.ReadLine();

        using (var context = new CountriesContext())
        {
            var query = from country in context.Countries
                        join city in context.Cities
                            on country.Id equals city.IdCountry
                        where country.CountryName == countryName
                        select new { city.CityName };

            TV.DisplayTable(query);
        }
    }

    // 5. Отобразить названия столиц с количеством жителей больше пяти миллионов
    public void ShowCapitalsWithPopulationOver5M()
    {
        using (var context = new CountriesContext())
        {
            var query = from country in context.Countries
                        join capitalCity in context.Cities
                            on country.IdCapitalCity equals capitalCity.Id
                        where capitalCity.CityPopulation > 5000000
                        select new { capitalCity.CityName };

            TV.DisplayTable(query);
        }
    }

    // 6. Отобразить названия всех европейских стран
    public void ShowEuropeanCountries()
    {
        using (var context = new CountriesContext())
        {
            var query = from country in context.Countries
                        join continent in context.Continents
                            on country.IdContinent equals continent.Id
                        where continent.ContinentName == "Европа"
                        select new { country.CountryName };

            TV.DisplayTable(query);
        }
    }

    // 7. Отобразить названия стран с площадью больше конкретного числа
    public void ShowCountriesByArea()
    {
        Console.WriteLine("Введите минимальную площадь:");
        if (decimal.TryParse(Console.ReadLine(), out var area))
        {
            using (var context = new CountriesContext())
            {
                var query = from country in context.Countries
                            where country.Area > area
                            select new { country.CountryName };

                TV.DisplayTable(query);
            }
        }
        else
        {
            WindowsHandler.AddInfoWindow(["Неверный ввод площади."]);
        }
    }

    public enum ProgramOptions
    {
        ShowAllCountry,
        ShowCountryNames,
        ShowCapitalNames,
        ShowCitiesForCountry,
        ShowCapitalsWithPopulationOver5M,
        ShowEuropeanCountries,
        ShowCountriesByArea,
        Back,
        CountOptions
    }
}
