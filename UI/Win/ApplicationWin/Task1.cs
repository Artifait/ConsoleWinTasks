using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.UI.Win.WinTemplate;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task1 : CwTask
    {
        #region GeneratedCode
        public enum ProgramOptions
        {
            Back,
            StartProcces
        }

        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public Task1() : base(nameof(Task1))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler },
                { (int)ProgramOptions.StartProcces, Task1Handler },
            };
        }
        #endregion

        #region Logic

        private void Task1Handler()
        {
            string filePath = IND.InputProperty("путь к exe(Пример: notepad.exe)");
            using (Process process = new())
            {
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = false; // Отключение использования оболочки
                process.StartInfo.CreateNoWindow = false; // Запуск без окна, если не нужно отображать

                try
                {
                    process.Start();
                    Console.WriteLine("Дочерний процесс запущен. Ожидание завершения...");

                    process.WaitForExit();

                    WindowsHandler.AddInfoWindow([$"Процесс завершен. Код завершения: {process.ExitCode}"]);
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
