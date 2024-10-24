﻿using ConsoleWinTasks.UI.ConsoleFrameWork;
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
                { (int)ProgramOptions.WinRegistration , WindowsHandler.AddWindow<WinRegistration> },
            };
        }

        public enum ProgramOptions
        {
            WinRegistration,
            Back
        }
    }
}