using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.UI.Win.WinTemplate;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class WinStart : CwTask
    {
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public WinStart() : base(nameof(WinStart))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back , BackHandler },
                { (int)ProgramOptions.Task1 , WindowsHandler.AddWindow<Task1> },
                { (int)ProgramOptions.Task2 , WindowsHandler.AddWindow<Task2> },
                { (int)ProgramOptions.Task3 , WindowsHandler.AddWindow<Task3> },

            };
        }

        public enum ProgramOptions
        {
            Task1,
            Task2,
            Task3,
            Back
        }
    }
}