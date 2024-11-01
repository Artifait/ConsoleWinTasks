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
                { (int)ProgramOptions.Back, BackHandler },
                { (int)ProgramOptions.Task1, WindowsHandler.ToWindow<Task1> },
                { (int)ProgramOptions.Task2, WindowsHandler.ToWindow<Task2> },
                { (int)ProgramOptions.Task3, WindowsHandler.ToWindow<Task3> },
                { (int)ProgramOptions.Task4, WindowsHandler.ToWindow<Task4> },
                { (int)ProgramOptions.ToFiches, WindowsHandler.ToWindow<Fiches> },
                { (int)ProgramOptions.Exit666, () => Environment.Exit(666) },
            };
        }

        public enum ProgramOptions
        {
            Task1,
            Task2,
            Task3,
            Task4,
            ToFiches,
            Exit666,
            Back
        }
    }
}