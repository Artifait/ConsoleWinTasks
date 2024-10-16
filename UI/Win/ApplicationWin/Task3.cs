
namespace ConsoleWinTasks.UI.Win.ApplicationWin;

public class Task3 : IWin
{
    #region DefaultPart
    public WindowDisplay windowDisplay = new("Task 3. Test Functional", typeof(ProgramOptions));

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
            case ProgramOptions.InsertData:
                InsertData();
                break;
            case ProgramOptions.ViewAllData:
                ViewAllData();
                break;

            case ProgramOptions.Back:
                Application.WinStack.Pop();
                break;
        }
    }

    public void ViewAllData()
    {
        var res = ((GameContext)Application.db).Games.ToList();

        TV.DisplayTable(res);
    }

    public void InsertData()
    {
        List<Game> portalGamesAndMods = [
            new Game {
                Name = "Portal",
                StuleGame = "Puzzle",
                ReleaseDate = new DateOnly(2007, 10, 10)
            },
            new Game {
                Name = "Portal 2",
                StuleGame = "Puzzle",
                ReleaseDate = new DateOnly(2011, 04, 18)
            },
            new Game {
                Name = "Portal Stories: Mel",
                StuleGame = "Puzzle",
                ReleaseDate = new DateOnly(2015, 06, 25)
            },
            new Game {
                Name = "Aperture Tag: The Paint Gun Testing Initiative",
                StuleGame = "Puzzle",
                ReleaseDate = new DateOnly(2014, 12, 02)
            },
            new Game {
                Name = "Portal: Still Alive",
                StuleGame = "Puzzle",
                ReleaseDate = new DateOnly(2008, 10, 22)
            },
            new Game {
                Name = "Portal: Companion Collection",
                StuleGame = "Puzzle",
                ReleaseDate = new DateOnly(2022, 08, 30) 
            }
        ];

        Application.db.AddRange(portalGamesAndMods);
        Application.db.SaveChanges();
    }

    public enum ProgramOptions
    {
        InsertData,
        ViewAllData,
        Back,
        CountOptions
    }
}