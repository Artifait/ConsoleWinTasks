using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.AppLogic;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task1 : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            HelloWorld,
        }
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public Task1() : base(nameof(Task1))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.HelloWorld, HelloWorldHandler },  
            };
        }
        #endregion

        #region Logic 
 
        private void HelloWorldHandler()
        {
            WinApiManager.MessageBox((int)WinApiManager.TypeWindow.MB_OK, "Hello World!", "BASA", 0);
        }
        #endregion
    }
}
