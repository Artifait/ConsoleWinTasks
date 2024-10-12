using Microsoft.Data.SqlClient;
using QuizTop;
using QuizTop.UI;
using static QuizTop.UI.Win.ApplicationWin.WinStart;

namespace ConsoleWinApp.UI.Win.ApplicationWin;

public class Task3 : IWin
{
    public WindowDisplay windowDisplay = new("Task 3: Display Operations", typeof(ProgramOptions));

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
            case ProgramOptions.DisplayAllCoffee:
                DisplayAllCoffee(db);
                break;

            case ProgramOptions.DisplayCoffeeNames:
                DisplayCoffeeNames(db);
                break;

            case ProgramOptions.DisplayArabica:
                DisplayArabica(db);
                break;

            case ProgramOptions.DisplayRobusta:
                DisplayRobusta(db);
                break;

            case ProgramOptions.DisplayBlends:
                DisplayBlends(db);
                break;

            case ProgramOptions.DisplayLowStockCoffee:
                DisplayLowStockCoffee(db);
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    private void DisplayAllCoffee(PointDB db)
    {
        string query = "SELECT CoffeeID, CoffeeName, CountryName, CoffeeTypeName, Description, QuantityInGrams, CostPrice " +
                       "FROM Coffee " +
                       "JOIN Countries ON Coffee.CountryID = Countries.Id " +
                       "JOIN CoffeeTypes ON Coffee.CoffeeTypeID = CoffeeTypes.Id";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayCoffeeNames(PointDB db)
    {
        string query = "SELECT CoffeeName FROM Coffee";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayArabica(PointDB db)
    {
        string query = "SELECT CoffeeName FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Arabica')";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayRobusta(PointDB db)
    {
        string query = "SELECT CoffeeName FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Robusta')";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayBlends(PointDB db)
    {
        string query = "SELECT CoffeeName FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Blend')";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayLowStockCoffee(PointDB db)
    {
        string query = "SELECT CoffeeName FROM Coffee WHERE QuantityInGrams <= 200";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    public enum ProgramOptions
    {
        DisplayAllCoffee,
        DisplayCoffeeNames,
        DisplayArabica,
        DisplayRobusta,
        DisplayBlends,
        DisplayLowStockCoffee,
        Back,
        CountOptions
    }
}