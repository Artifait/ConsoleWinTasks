
using QuizTop.UI;
using QuizTop.UI.Win.ApplicationWin;
using System.Data;

#nullable enable
namespace QuizTop
{
    public static class Application
    {
        public static Stack<IWin> WinStack = new();
        public static bool IsRunning = false;
        public static string PathData = Directory.GetCurrentDirectory();
        public static PointDB db = new("CoffeeShop");
        public static bool CursorVisible { get; set; } = false;

        private static void Init()
        {
            Console.Title = "Tasks";
            Console.SetWindowSize(150, 40);
            WinStack.Push(WindowsHandler.GetWindow<WinStart>());

            db.OpenDB();
            if (!db.IsWork())
            {
                Console.Clear();
                Console.WriteLine("НЕ удалось открыть бд");
                return;
            }
            else { WindowsHandler.AddInfoWindow(["БД Открылась!:)"]); }
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
}
