using ConsoleWinApp.UI;
using ConsoleWinTasks;
using QuizTop;
using QuizTop.UI;

namespace ConsoleWinTasks.UI.Win.ApplicationWin;

public class Task3 : IWin
{
    #region DefaultPart
    public WindowDisplay windowDisplay = new("Task 3: Show data", typeof(ProgramOptions));

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
                ShowCapitalsWithAP(); 
                break;

            case ProgramOptions.ShowCapitalsStartingWithK:
                ShowCapitalsStartingWithK(); 
                break;

            case ProgramOptions.ShowCountriesInAreaRange:
                ShowCountriesInAreaRange();
                break;

            case ProgramOptions.ShowCountriesWithPopulationOver:
                ShowCountriesWithPopulationOver();
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

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
        ShowCapitalsWithAP,              
        ShowCapitalsStartingWithK,       
        ShowCountriesInAreaRange,          
        ShowCountriesWithPopulationOver,   
        Back,
        CountOptions
    }
}