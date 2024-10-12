using QuizTop;
using QuizTop.UI;

namespace ConsoleWinApp.UI.Win.ApplicationWin;

public class Task2 : IWin
{
    public WindowDisplay windowDisplay = new("Task 2: Coffee Information", typeof(ProgramOptions));

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
            case ProgramOptions.DisplayCherryCoffee:
                DisplayCherryCoffee(db);
                break;

            case ProgramOptions.DisplayCostRangeCoffee:
                Console.WriteLine("Cin min and max cost:");
                decimal min = Convert.ToDecimal(Console.ReadLine());
                decimal max = Convert.ToDecimal(Console.ReadLine());
                DisplayCostRangeCoffee(db, min, max); 
                break;

            case ProgramOptions.DisplayQuantityRangeCoffee:
                DisplayQuantityRangeCoffee(db, 500, 1000); // Укажите диапазон
                break;

            case ProgramOptions.DisplayCountryCoffee:
                DisplayCountryCoffee(db, "Colombia", "Brazil"); // Укажите страны
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    private void DisplayCherryCoffee(PointDB db)
    {
        string query = "SELECT * FROM Coffee WHERE Description LIKE '%cherry%'";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayCostRangeCoffee(PointDB db, decimal minCost, decimal maxCost)
    {
        string query = $"SELECT * FROM Coffee WHERE CostPrice BETWEEN {minCost} AND {maxCost}";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayQuantityRangeCoffee(PointDB db, int minQuantity, int maxQuantity)
    {
        string query = $"SELECT * FROM Coffee WHERE QuantityInGrams BETWEEN {minQuantity} AND {maxQuantity}";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayCountryCoffee(PointDB db, params string[] countries)
    {
        string countryList = string.Join(", ", countries.Select(c => $"'{c}'"));
        string query = $"SELECT * FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id WHERE CountryName IN ({countryList})";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    public enum ProgramOptions
    {
        DisplayCherryCoffee,
        DisplayCostRangeCoffee,
        DisplayQuantityRangeCoffee,
        DisplayCountryCoffee,
        Back,
        CountOptions
    }
}
