using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.AppLogic;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task2 : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Play,
         }
        public override Type? ProgramOptionsType => typeof(ProgramOptions);
 
        public Task2() : base(nameof(Task2))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.Play, PlayHandler },  
            };
        }
        #endregion

        #region Logic 

        private const uint MB_YESNO = (uint)WinApiManager.TypeWindow.MB_YESNO;
        private const uint MB_YESNOCANCEL = (uint)WinApiManager.TypeWindow.MB_YESNOCANCEL;
        private const uint IDYES = (uint)WinApiManager.TypeAnswer.YES;
        private const uint IDNO = (uint)WinApiManager.TypeAnswer.NO;
        private const uint IDCANCEL = (uint)WinApiManager.TypeAnswer.CANCEL;
        private void PlayHandler()
        {
            int min = 0;
            int max = 100;
            bool guessed = false;

            WinApiManager.MessageBox(IntPtr.Zero, "Загадайте число от 0 до 100.", "Игра", 0);

            while (!guessed)
            {
                int guess = (min + max) / 2;

                string message = $"Ваше число {guess}?";
                int result = WinApiManager.MessageBox(IntPtr.Zero, message, "Угадываем", MB_YESNOCANCEL);

                if (result == IDYES)
                {
                    WinApiManager.MessageBox(IntPtr.Zero, $"Число угадано! Это {guess}.", "Поздравление", 0);
                    guessed = true;
                }
                else if (result == IDNO)
                {
                    result = WinApiManager.MessageBox(IntPtr.Zero, "Ваше число больше?", "Уточнение", MB_YESNO);

                    if (result == IDYES)
                    {
                        min = guess + 1;
                    }
                    else
                    {
                        max = guess - 1;
                    }
                }
                else if (result == IDCANCEL)
                {
                    WinApiManager.MessageBox(IntPtr.Zero, "Игра отменена.", "Отмена", 0);
                    guessed = true;
                }
            }
        }
        #endregion
    }
}
