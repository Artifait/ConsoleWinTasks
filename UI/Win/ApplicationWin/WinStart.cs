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
                { (int)ProgramOptions.Demo , WindowsHandler.AddWindow<Demo> },
            };
        }

        public enum ProgramOptions
        {
            Demo,
            Back
        }
    }
}