
using ConsoleWinTasks.UI.ConsoleFrameWork;
using Microsoft.EntityFrameworkCore;

namespace ConsoleWinTasks;

public static class Application
{
    public static Stack<IWin> WinStack = new();
    public static bool IsRunning = false;
    public static string PathData = Directory.GetCurrentDirectory();
    public static bool CursorVisible { get; set; } = false;

    private static void Init()
    {
        Console.Title = "Tasks";
        Console.SetWindowSize(80, 40);
        WinStack.Push(WindowsHandler.GetWindow<UI.Win.ApplicationWin.WinStart>());
    }

    public static void Run()
    {
        if (IsRunning) return;
        IsRunning = true;
        Init();

        while (IsRunning && WinStack.Count > 0)
        {
            WinStack.Peek().Show();
            Console.CursorVisible = CursorVisible;
            try { WinStack.Peek().InputHandler(); }
            catch (Exception ex) { WindowsHandler.AddErroreWindow([ex.Message]); }
        }
    }
}
