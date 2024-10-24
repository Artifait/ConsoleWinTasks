using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class MainWindow : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            LoadProcess,
            ShowAllProcess,
            ShowOursProcess,
            AddYourProcess,
            KillYourProcess,
            KillNotepad,
            Zapac3,
 
        }

        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public MainWindow() : base(nameof(MainWindow))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.LoadProcess, LoadProcessHandler },  
                { (int)ProgramOptions.ShowAllProcess, ShowAllProcessHandler },  
                { (int)ProgramOptions.ShowOursProcess, ShowOursProcessHandler },  
                { (int)ProgramOptions.AddYourProcess, AddYourProcessHandler },  
                { (int)ProgramOptions.KillYourProcess, KillYourProcessHandler },  
                { (int)ProgramOptions.KillNotepad, KillNotepad },  
                { (int)ProgramOptions.Zapac3, Zapac3Handler },  
            };
        }
        public override void HandleMenuOption()
        {
            Console.Clear();
            base.HandleMenuOption();
        }
        #endregion

        #region Logic 

        private List<Process> allProcesses = [];

        private List<Process> userProcesses = [];

        private void LoadProcessHandler()
        {
            Console.WriteLine("Loading all processes...");
            UpdateAllProcess();
            Console.WriteLine($"Total processes loaded: {allProcesses.Count}");
        }

        private void UpdateAllProcess()
            => allProcesses = [.. Process.GetProcesses()]; 
        
        private void ShowAllProcessHandler()
        {
            UpdateAllProcess();
            TV.DisplayTable(allProcesses.Select(g => new { g.Id, g.ProcessName }).ToList());
        }

        private void ShowOursProcessHandler()
        {
            Console.WriteLine("Showing user-created processes:");
            if (userProcesses.Count == 0)
            {
                WindowsHandler.AddInfoWindow(["No user processes found."]);
                return;
            }

            TV.DisplayTable(userProcesses.Select(g => new { g.Id, g.ProcessName }).ToList());

            string input = IND.InputProperty("Enter the Process ID to view details:");
            if (int.TryParse(input, out int selectedProcessId))
            {
                Process selectedProcess = userProcesses.FirstOrDefault(p => p.Id == selectedProcessId);
                if (selectedProcess != null)
                {
                    try
                    {
                        Console.WriteLine($"\nDetailed Information for Process ID {selectedProcessId}:");
                        Console.WriteLine($"Process Name: {selectedProcess.ProcessName}");
                        Console.WriteLine($"Start Time: {selectedProcess.StartTime}");
                        Console.WriteLine($"Total Processor Time: {selectedProcess.TotalProcessorTime}");
                        Console.WriteLine($"Thread Count: {selectedProcess.Threads.Count}");

                        int processCount = Process.GetProcessesByName(selectedProcess.ProcessName).Length;
                        Console.WriteLine($"■ Number of Instances: {processCount}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error fetching process details: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Process not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Process ID.");
            }
        }


        private void AddYourProcessHandler()
        {
            Process pr = new();
            string userChoice = IND.InputProperty("1 - notepad, 2 - calc, 3 - Paint, 4 - Path to Program");

            switch (int.Parse(userChoice))
            {
                case 1:
                    pr.StartInfo = new ProcessStartInfo("notepad.exe");
                    break;
                case 2:
                    pr.StartInfo = new ProcessStartInfo("calc.exe");
                    break;
                case 3:
                    pr.StartInfo = new ProcessStartInfo("mspaint.exe");
                    break;
                case 4:
                    string customPath = IND.InputProperty("Enter the full path to the program:");
                    pr.StartInfo = new ProcessStartInfo(customPath);
                    break;
                default:
                    Console.WriteLine("Invalid option, process not started.");
                    return;
            }

            try
            {
                pr.Start();
                userProcesses.Add(pr);
                WindowsHandler.AddInfoWindow([$"Process {pr.ProcessName} started with ID: {pr.Id}"]);
            }
            catch (Exception ex)
            {
                WindowsHandler.AddInfoWindow([$"Failed to start process: {ex.Message}"]);
            }
        }

        private void KillYourProcessHandler()
        {
            if (userProcesses.Count == 0)
            {
                WindowsHandler.AddInfoWindow(["No user processes to kill."]);
                return;
            }
            TV.DisplayTable(userProcesses.Select(g => new { g.Id, g.ProcessName }).ToList());
            int idPr = int.Parse(IND.InputProperty("ID Процесса"));
            Process selectPr = userProcesses.Where(g => g.Id == idPr).FirstOrDefault();
            if(selectPr != null)
            {
                userProcesses.Remove(selectPr);
                selectPr.Kill();
                WindowsHandler.AddInfoWindow([$"Killed process {selectPr.Id}: {selectPr.ProcessName}"]);
            }
            else
                WindowsHandler.AddInfoWindow([$"Нету такого процесса"]);
        }
        private void Zapac3Handler()
        {
            Console.WriteLine("Zapac3Handler");
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010;

        private void KillNotepad()
        {
            IntPtr hWnd = FindWindow("Notepad", null);

            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Notepad window not found.");
            }
            else
            {
                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine("Close message sent to Notepad.");
            }
        }
        #endregion
    }
}
