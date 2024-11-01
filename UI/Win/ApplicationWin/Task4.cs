using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using System.Diagnostics;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task4 : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Start,
 
        }

        public override Type? ProgramOptionsType => typeof(ProgramOptions);

 
        public Task4() : base(nameof(Task4))
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
            string filePath = IND.InputProperty("Введите путь к файлу (Пример: E:\\someFolder\\file.txt)");
            string searchWord = IND.InputProperty("Введите слово для поиска");

            string childProcessPath = @"..\..\..\AppLogic\FileSearchProcess\ChildProcessApp.exe";

            using (Process process = new())
            {
                process.StartInfo.FileName = childProcessPath;
                process.StartInfo.Arguments = $"\"{filePath}\" \"{searchWord}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                try
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Console.WriteLine("Результат от дочернего процесса: " + output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при запуске дочернего процесса: {ex.Message}");
                }
            }
        }
        #endregion
    }
}
