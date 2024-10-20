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
                { (int)ProgramOptions.CwTask1 , WindowsHandler.AddWindow<CwTask1>},
                { (int)ProgramOptions.CwTask2 , WindowsHandler.AddWindow<CwTask2>},
                { (int)ProgramOptions.CwTask3 , WindowsHandler.AddWindow<CwTask3>},
                { (int)ProgramOptions.Back , BackHandler },
            };
        }

        public enum ProgramOptions
        {
            CwTask1,
            CwTask2,
            CwTask3,
            Back
        }
    }
}