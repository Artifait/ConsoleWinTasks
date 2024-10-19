using ConsoleWinTasks.UI.ConsoleFrameWork;

namespace ConsoleWinTasks.UI.Win.WinTemplate
{
    public abstract class CwTask : IWin
    {
        public WindowDisplay windowDisplay;

        public WindowDisplay WindowDisplay
        {
            get => windowDisplay;
            set => windowDisplay = value;
        }

        public virtual Type? ProgramOptionsType => null;
        public virtual Type? ProgramFieldsType => null;

        public int SizeX => windowDisplay.MaxLeft;
        public int SizeY => windowDisplay.MaxTop;

        public void Show() => windowDisplay.Show();

        public void InputHandler()
        {
            char lower = char.ToLower(Console.ReadKey().KeyChar);
            WindowTools.UpdateCursorPos(lower, ref windowDisplay, MenuHandlers.Count);

            if (WindowTools.IsKeySelect(lower)) HandleMenuOption();
        }

        public Dictionary<int, Action> MenuHandlers = [];


        public virtual void HandleMenuOption()
        {
            Console.CursorTop = SizeY;
            Console.CursorLeft = 0;
            int ch = windowDisplay.CursorPosition;
            MenuHandlers[ch]();
        }

        protected static void BackHandler()
        {
            Application.WinStack.Pop();
            Console.Clear();
        }

        public CwTask(string title = "ConsoleWin TASK")
        {
            windowDisplay = new(title, ProgramOptionsType, ProgramFieldsType);
        }

    }
}