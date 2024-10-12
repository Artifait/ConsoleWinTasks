using QuizTop;
using QuizTop.UI;
using static QuizTop.UI.Win.ApplicationWin.WinStart;

namespace ConsoleWinApp.UI.Win.ApplicationWin;

public class Task4 : IWin
{
    public WindowDisplay windowDisplay = new("Task 4: Top Coffee Stats", typeof(ProgramOptions));

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
            case ProgramOptions.DisplayTopCountriesByCoffeeCount:
                DisplayTopCountriesByCoffeeCount(db);
                break;

            case ProgramOptions.DisplayTopCountriesByQuantity:
                DisplayTopCountriesByQuantity(db);
                break;

            case ProgramOptions.DisplayTopArabicaByQuantity:
                DisplayTopArabicaByQuantity(db);
                break;

            case ProgramOptions.DisplayTopRobustaByQuantity:
                DisplayTopRobustaByQuantity(db);
                break;

            case ProgramOptions.DisplayTopBlendByQuantity:
                DisplayTopBlendByQuantity(db);
                break;

            case ProgramOptions.DisplayTopByTypeQuantity:
                DisplayTopByTypeQuantity(db);
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    private void DisplayTopCountriesByCoffeeCount(PointDB db)
    {
        string query = "SELECT TOP 3 CountryName, COUNT(CoffeeID) AS CoffeeCount FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id GROUP BY CountryName ORDER BY CoffeeCount DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopCountriesByQuantity(PointDB db)
    {
        string query = "SELECT TOP 3 CountryName, SUM(QuantityInGrams) AS TotalQuantity FROM Coffee JOIN Countries ON Coffee.CountryID = Countries.Id GROUP BY CountryName ORDER BY TotalQuantity DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopArabicaByQuantity(PointDB db)
    {
        string query = "SELECT TOP 3 CoffeeName, QuantityInGrams FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Arabica') ORDER BY QuantityInGrams DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopRobustaByQuantity(PointDB db)
    {
        string query = "SELECT TOP 3 CoffeeName, QuantityInGrams FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Robusta') ORDER BY QuantityInGrams DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopBlendByQuantity(PointDB db)
    {
        string query = "SELECT TOP 3 CoffeeName, QuantityInGrams FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Blend') ORDER BY QuantityInGrams DESC";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayTopByTypeQuantity(PointDB db)
    {
        string query = @"
                SELECT CoffeeTypeName, CoffeeName, QuantityInGrams 
                FROM (
                    SELECT CoffeeTypeName, CoffeeName, QuantityInGrams,
                           ROW_NUMBER() OVER (PARTITION BY CoffeeTypeID ORDER BY QuantityInGrams DESC) AS Rank
                    FROM Coffee 
                    JOIN CoffeeTypes ON Coffee.CoffeeTypeID = CoffeeTypes.Id
                ) AS RankedCoffee
                WHERE Rank <= 3";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    public enum ProgramOptions
    {
        DisplayTopCountriesByCoffeeCount,
        DisplayTopCountriesByQuantity,
        DisplayTopArabicaByQuantity,
        DisplayTopRobustaByQuantity,
        DisplayTopBlendByQuantity,
        DisplayTopByTypeQuantity,
        Back,
        CountOptions
    }
}

