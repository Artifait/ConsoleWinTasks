using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using System.Diagnostics;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task2 : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Start,
        }

        public override Type? ProgramOptionsType => typeof(ProgramOptions);

 
        public Task2() : base(nameof(Task2))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.Start, StartHandler },  
            };
        }
        #endregion

        #region Logic 
 
        private void StartHandler()
        {
            Console.WriteLine("e - Ожидать завершения процесса, иначе - Мы убьем его");
            if (WindowTools.IsKeySelect(Char.ToLower(Console.ReadKey(true).KeyChar)))
            {
                Task1.WaitEndAndViewCode();
                return;
            }
            string filePath = IND.InputProperty("путь к exe(Пример: notepad.exe)");
            using (Process process = new())
            {
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = false;

                try
                {
                    process.Start();
                    Console.WriteLine("Дочерний процесс запущен. Принудительное завершение через 5 секунд...");

                    Task.Delay(5000).Wait();

                    if (!process.HasExited)
                    {
                        process.Kill();
                        WindowsHandler.AddInfoWindow(["Процесс был принудительно завершен."]);
                    }
                    else
                    {
                        WindowsHandler.AddInfoWindow(["Процесс завершился сам до принудительного завершения."]);
                    }
                }
                catch (Exception ex)
                {
                    WindowsHandler.AddErroreWindow([$"Произошла ошибка: {ex.Message}"]);
                }
            }
        }
        #endregion
    }
}
