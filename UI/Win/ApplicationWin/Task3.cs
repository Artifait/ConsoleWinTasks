using QuizTop;
using QuizTop.UI;
using static QuizTop.UI.Win.ApplicationWin.WinStart;

namespace ConsoleWinApp.UI.Win.ApplicationWin;

public class Task3 : IWin
{
    public WindowDisplay windowDisplay = new("Task 3: Coffee Stats by Country", typeof(ProgramOptions));

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

    private void HandleMenuOption()
    {
        Console.Clear();
        PointDB db = Application.db;
        Console.CursorTop = SizeY;

        switch ((ProgramOptions)windowDisplay.CursorPosition)
        {
            case ProgramOptions.DisplayCountryCoffeeCount:
                DisplayCountryCoffeeCount(db);
                break;

            case ProgramOptions.DisplayAvgQuantityByCountry:
                DisplayAvgQuantityByCountry(db);
                break;

            case ProgramOptions.DisplayTopCheapCoffeeByCountry:
                DisplayTopCheapCoffeeByCountry(db, "Colombia"); // Укажите страну
                break;

            case ProgramOptions.DisplayTopExpensiveCoffeeByCountry:
                DisplayTopExpensiveCoffeeByCountry(db, "Brazil"); // Укажите страну
                break;

            case ProgramOptions.DisplayTopCheapCoffee:
                DisplayTopCheapCoffee(db);
                break;

            case ProgramOptions.DisplayTopExpensiveCoffee:
                DisplayTopExpensiveCoffee(db);
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    private void DisplayCountryCoffeeCount(PointDB db)
    {
        string query = "SELECT CountryName, COUNT(CoffeeID) AS CoffeeCount FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id GROUP BY CountryName";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayAvgQuantityByCountry(PointDB db)
    {
        string query = "SELECT CountryName, AVG(QuantityInGrams) AS AvgQuantity FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id GROUP BY CountryName";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopCheapCoffeeByCountry(PointDB db, string country)
    {
        string query = $"SELECT TOP 3 * FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id WHERE CountryName = '{country}' ORDER BY CostPrice ASC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopExpensiveCoffeeByCountry(PointDB db, string country)
    {
        string query = $"SELECT TOP 3 * FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id WHERE CountryName = '{country}' ORDER BY CostPrice DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopCheapCoffee(PointDB db)
    {
        string query = "SELECT TOP 3 * FROM Coffee ORDER BY CostPrice ASC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopExpensiveCoffee(PointDB db)
    {
        string query = "SELECT TOP 3 * FROM Coffee ORDER BY CostPrice DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    public enum ProgramOptions
    {
        DisplayCountryCoffeeCount,
        DisplayAvgQuantityByCountry,
        DisplayTopCheapCoffeeByCountry,
        DisplayTopExpensiveCoffeeByCountry,
        DisplayTopCheapCoffee,
        DisplayTopExpensiveCoffee,
        Back,
        CountOptions
    }
}
