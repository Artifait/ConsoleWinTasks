using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using System.Diagnostics;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ConsoleWinTasks.AppLogic;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Demo : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Add,
            Minus,
            Multiply,
            Power
        }
        CalculatorInvoker calc = new();
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public Demo() : base(nameof(Demo))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.Add, AddHandler },
                { (int)ProgramOptions.Minus, MinusHandler },
                { (int)ProgramOptions.Multiply, MultiplyHandler },
                { (int)ProgramOptions.Power, PowerHandler },

            };
        }
        #endregion

        #region Logic 
        public enum TypeWindow
        {
            MB_ABORTRETRYIGNORE = (int)0x00000002L,
            MB_CANCELTRYCONTINUE = (int)0x00000006L,
            MB_HELP = (int)0x00004000L,
            MB_OK = (int)0x00000000L,
            MB_OKCANCEL = (int)0x00000001L,
            MB_RETRYCANCEL = (int)0x00000005L,
            MB_YESNO = (int)0x00000004L,
            MB_YESNOCANCEL = (int)0x00000003L
        }
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        private void ShowResult(string str)
            => WindowsHandler.AddInfoWindow(["Result: " + str]);
        private void AddHandler()
            => ShowResult(calc.Add(int.Parse(IND.InputProperty("A")), int.Parse(IND.InputProperty("B"))).ToString());
        private void MinusHandler()
            => ShowResult(calc.Subtract(int.Parse(IND.InputProperty("A")), int.Parse(IND.InputProperty("B"))).ToString());
        private void MultiplyHandler()
            => ShowResult(calc.Multiply(int.Parse(IND.InputProperty("A")), int.Parse(IND.InputProperty("B"))).ToString());
        private void PowerHandler()
            => ShowResult(calc.Power(int.Parse(IND.InputProperty("A")), int.Parse(IND.InputProperty("B"))).ToString());


        #endregion
    }
}