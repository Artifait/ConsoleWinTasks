using ConsoleWinApp.UI;
using ConsoleWinTasks;
using QuizTop;
using QuizTop.UI;
using System.Threading.Tasks;

namespace ConsoleWinTasks.UI.Win.ApplicationWin;



public class Task3 : IWin
{
    #region DefaultPart
    public WindowDisplay windowDisplay = new("Task 1: Insert Operations", typeof(ProgramOptions));

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
            // Задание 3
            case ProgramOptions.ShowCapitalsWithAP:
                ShowCapitalsWithAP(); // Используем Task3
                break;

            case ProgramOptions.ShowCapitalsStartingWithK:
                ShowCapitalsStartingWithK(); // Используем Task3
                break;

            case ProgramOptions.ShowCountriesInAreaRange:
                ShowCountriesInAreaRange(); // Используем Task3
                break;

            case ProgramOptions.ShowCountriesWithPopulationOver:
                ShowCountriesWithPopulationOver(); // Используем Task3
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    // 1. Отобразить все столицы, у которых в названии есть буквы a, p
    public void ShowCapitalsWithAP()
    {
        using (var context = new CountriesContext())
        {
            var query = from capital in context.Cities
                        where capital.CityName.Contains('a') || capital.CityName.Contains('р')
                        select new { capital.CityName };

            TV.DisplayTable(query);
        }
    }

    // 2. Отобразить все столицы, у которых название начинается с буквы k
    public void ShowCapitalsStartingWithK()
    {
        using (var context = new CountriesContext())
        {
            var query = from capital in context.Cities
                        where capital.CityName.StartsWith("K")
                        select new { capital.CityName };

            TV.DisplayTable(query);
        }
    }

    // 3. Отобразить название стран, у которых площадь находится в указанном диапазоне
    public void ShowCountriesInAreaRange()
    {
        Console.WriteLine("Введите минимальную площадь:");
        if (decimal.TryParse(Console.ReadLine(), out var minArea))
        {
            Console.WriteLine("Введите максимальную площадь:");
            if (decimal.TryParse(Console.ReadLine(), out var maxArea))
            {
                using (var context = new CountriesContext())
                {
                    var query = from country in context.Countries
                                where country.Area >= minArea && country.Area <= maxArea
                                select new { country.CountryName };

                    TV.DisplayTable(query);
                }
            }
            else
            {
                WindowsHandler.AddInfoWindow(new[] { "Неверный ввод максимальной площади." });
            }
        }
        else
        {
            WindowsHandler.AddInfoWindow(new[] { "Неверный ввод минимальной площади." });
        }
    }

    // 4. Отобразить название стран, у которых количество жителей больше указанного числа
    public void ShowCountriesWithPopulationOver()
    {
        Console.WriteLine("Введите минимальное количество жителей:");
        if (int.TryParse(Console.ReadLine(), out var population))
        {
            using (var context = new CountriesContext())
            {
                var query = from country in context.Countries
                            where country.Population > population
                            select new { country.CountryName };

                TV.DisplayTable(query);
            }
        }
        else
        {
            WindowsHandler.AddInfoWindow(new[] { "Неверный ввод населения." });
        }
    }
    public enum ProgramOptions
    {
        ShowCapitalsWithAP,                // Добавлено
        ShowCapitalsStartingWithK,         // Добавлено
        ShowCountriesInAreaRange,          // Добавлено
        ShowCountriesWithPopulationOver,    // Добавлено
        Back,
        CountOptions
    }
}