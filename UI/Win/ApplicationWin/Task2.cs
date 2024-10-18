
using ConsoleWinApp.UI;
using QuizTop;
using QuizTop.UI;
using System.Diagnostics;

namespace ConsoleWinTasks.UI.Win.ApplicationWin;

public class Task2 : IWin
{
    #region DefaultPart
    public WindowDisplay windowDisplay = new("Task 2: Reports System", typeof(ProgramOptions));

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

        Console.CursorTop = SizeY;
        switch ((ProgramOptions)windowDisplay.CursorPosition)
        {
            case ProgramOptions.ShowFullCountryInfo:
                ShowFullCountryInfo();
                break;

            case ProgramOptions.ShowPartialCountryInfo:
                ShowPartialCountryInfo();
                break;

            case ProgramOptions.ShowSpecificCountryInfo:
                ShowSpecificCountryInfo();
                break;

            case ProgramOptions.ShowCitiesOfSpecificCountry:
                ShowCitiesOfSpecificCountry();
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    public void ShowFullCountryInfo()
    {
        Console.Write("Выберите вывод на экран (E) или в файл (F):");
        char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        using (var context = new CountriesContext())
        {
            var countries = context.Countries
                                   .Select(c => new { c.CountryName, c.Population, c.Area, c.CapitalName })
                                   .ToList();

            OutputReport(countries, outputChoice, "FullCountryInfo.txt");
        }
    }

    public void ShowPartialCountryInfo()
    {
        Console.Write("Введите количество полей для отображения (1 - только название, 2 - название и население, 3 - название, население и площадь):");
        if (int.TryParse(Console.ReadLine(), out int fields))
        {
            Console.Write("Выберите вывод на экран (E) или в файл (F):");
            char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            using (var context = new CountriesContext())
            {
                IQueryable<dynamic> query;
                switch (fields)
                {
                    case 1:
                        query = context.Countries.Select(c => new { c.CountryName });
                        break;
                    case 2:
                        query = context.Countries.Select(c => new { c.CountryName, c.Population });
                        break;
                    case 3:
                        query = context.Countries.Select(c => new { c.CountryName, c.Population, c.Area });
                        break;
                    default:
                        Console.WriteLine("Некорректное количество полей.");
                        return;
                }

                var countries = query.ToList();
                OutputReport(countries, outputChoice, "PartialCountryInfo.txt");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод.");
        }
    }

    public void ShowSpecificCountryInfo()
    {
        Console.WriteLine("Введите название страны:");
        string countryName = Console.ReadLine();

        Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
        char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

        using (var context = new CountriesContext())
        {
            var country = context.Countries
                                 .Where(c => c.CountryName == countryName)
                                 .Select(c => new { c.CountryName, c.Population, c.Area, c.CapitalName })
                                 .FirstOrDefault();

            if (country != null)
            {
                OutputReport(new[] { country }, outputChoice, $"{countryName}_Info.txt");
            }
            else
            {
                Console.WriteLine($"Страна '{countryName}' не найдена.");
            }
        }
    }

    public void ShowCitiesOfSpecificCountry()
    {
        Console.WriteLine("Введите название страны:");
        string countryName = Console.ReadLine();

        Console.WriteLine("Выберите вывод на экран (E) или в файл (F):");
        char outputChoice = char.ToUpper(Console.ReadKey().KeyChar);

        using (var context = new CountriesContext())
        {
            var cities = context.Cities
                                .Where(c => c.IdCountryNavigation.CountryName == countryName)
                                .Select(c => new { c.CityName, c.CityPopulation })
                                .ToList();

            if (cities.Any())
            {
                OutputReport(cities, outputChoice, $"{countryName}_CitiesInfo.txt");
            }
            else
            {
                Console.WriteLine($"Для страны '{countryName}' не найдено городов.");
            }
        }
    }

    public static void OutputReport<T>(ICollection<T> data, char outputChoice, string fileName)
    {
        if (WindowTools.IsKeyFromArray(['E', 'Е', 'У', 'T'], Char.ToUpper(outputChoice)))
        {
            TV.DisplayTable(data);
        }
        else if (WindowTools.IsKeyFromArray(['F', 'А'], Char.ToUpper(outputChoice)))
        {
            using (StreamWriter writer = new(Application.PathData + "\\" + fileName))
            {
                TV.DisplayTable(data, writer);
            }
            Console.WriteLine($"Отчет сохранен в файл: {Application.PathData}\\{fileName}");
            Process.Start("explorer.exe", Application.PathData);
        }
        else
        {
            Console.WriteLine("Некорректный выбор.");
        }
    }

    public enum ProgramOptions
    {
        ShowFullCountryInfo,
        ShowPartialCountryInfo,
        ShowSpecificCountryInfo,
        ShowCitiesOfSpecificCountry,
        Back,
        CountOptions
    }
}
