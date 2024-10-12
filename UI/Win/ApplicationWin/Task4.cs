using QuizTop;
using QuizTop.UI;
using static QuizTop.UI.Win.ApplicationWin.WinStart;

namespace ConsoleWinApp.UI.Win.ApplicationWin;

public class Task4 : IWin
{
    public WindowDisplay windowDisplay = new("Task 4: Coffee Statistics", typeof(ProgramOptions));

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
            case ProgramOptions.DisplayMinCostPrice:
                DisplayMinCostPrice(db);
                break;

            case ProgramOptions.DisplayMaxCostPrice:
                DisplayMaxCostPrice(db);
                break;

            case ProgramOptions.DisplayAvgCostPrice:
                DisplayAvgCostPrice(db);
                break;

            case ProgramOptions.DisplayCountMinCostPrice:
                DisplayCountMinCostPrice(db);
                break;

            case ProgramOptions.DisplayCountMaxCostPrice:
                DisplayCountMaxCostPrice(db);
                break;

            case ProgramOptions.DisplayCoffeeTypeCount:
                DisplayCoffeeTypeCount(db);
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    private void DisplayMinCostPrice(PointDB db)
    {
        string query = "SELECT MIN(CostPrice) AS MinCostPrice FROM Coffee";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayMaxCostPrice(PointDB db)
    {
        string query = "SELECT MAX(CostPrice) AS MaxCostPrice FROM Coffee";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayAvgCostPrice(PointDB db)
    {
        string query = "SELECT AVG(CostPrice) AS AvgCostPrice FROM Coffee";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayCountMinCostPrice(PointDB db)
    {
        string query = "SELECT COUNT(*) AS CountMinCostPrice FROM Coffee WHERE CostPrice = (SELECT MIN(CostPrice) FROM Coffee)";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayCountMaxCostPrice(PointDB db)
    {
        string query = "SELECT COUNT(*) AS CountMaxCostPrice FROM Coffee WHERE CostPrice = (SELECT MAX(CostPrice) FROM Coffee)";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    private void DisplayCoffeeTypeCount(PointDB db)
    {
        string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Arabica')) AS ArabicaCount,
                    (SELECT COUNT(*) FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Robusta')) AS RobustaCount,
                    (SELECT COUNT(*) FROM Coffee WHERE CoffeeTypeID = (SELECT Id FROM CoffeeTypes WHERE CoffeeTypeName = 'Blend')) AS BlendCount";
        TV.DisplayTable(db.ExecuteQuery(query));
    }

    public enum ProgramOptions
    {
        DisplayMinCostPrice,
        DisplayMaxCostPrice,
        DisplayAvgCostPrice,
        DisplayCountMinCostPrice,
        DisplayCountMaxCostPrice,
        DisplayCoffeeTypeCount,
        Back,
        CountOptions
    }
}
