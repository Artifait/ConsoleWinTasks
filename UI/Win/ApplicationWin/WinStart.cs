
#nullable enable

using ConsoleWinApp.UI.Win.ApplicationWin;

namespace QuizTop.UI.Win.ApplicationWin
{
    public class WinStart : IWin
    {
        public WindowDisplay windowDisplay = new("Tasks", typeof(ProgramOptions));

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

            if (WindowTools.IsKeySelect(lower)) HandlerMetodMenu();
        }

        private void HandlerMetodMenu()
        {
            Console.Clear();
            switch ((ProgramOptions)windowDisplay.CursorPosition)
            {
                case ProgramOptions.Task1:
                    Application.WinStack.Push(WindowsHandler.GetWindow<Task1>());
                    break;
                case ProgramOptions.Task2:
                    Application.WinStack.Push(WindowsHandler.GetWindow<Task2>());
                    break;
                case ProgramOptions.Task3:
                    Application.WinStack.Push(WindowsHandler.GetWindow<Task3>());
                    break;
                case ProgramOptions.Task4:
                    Application.WinStack.Push(WindowsHandler.GetWindow<Task4>());
                    break;
                case ProgramOptions.Exit:
                    Application.IsRunning = false;
                    break;
            }
        }

        public enum ProgramOptions
        {
            Task1,
            Task2,
            Task3,  
            Task4,
            Exit,
            CountOptions,
        }
    }
}
