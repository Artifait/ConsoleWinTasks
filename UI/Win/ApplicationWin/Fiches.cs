using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.AppLogic;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Fiches : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            StartBouncing,
            StopBouncing,

            InputSpeedX,
            InputSpeedY,
            InputDelay,
        }
        public enum ProgramFields
        {
            SpeedX,
            SpeedY,
            Delay,
        } 

        public override Type? ProgramFieldsType => typeof(ProgramFields);        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public string FdSpeedX
        {
            get => WindowDisplay.GetOrCreateField("SpeedX", "0");
            set {
                WindowDisplay.AddOrUpdateField("SpeedX", value);
                ConsoleWindowBouncer.dx = int.Parse(value);
            }
        } 
        public string FdSpeedY
        {
            get => WindowDisplay.GetOrCreateField("SpeedY", "0");
            set {
                WindowDisplay.AddOrUpdateField("SpeedY", value);
                ConsoleWindowBouncer.dy = int.Parse(value);
            }
        } 
        public string FdDelay
        {
            get => WindowDisplay.GetOrCreateField("Delay", "0");
            set
            {
                ConsoleWindowBouncer.Delay = int.Parse(value);
                WindowDisplay.AddOrUpdateField("Delay", ConsoleWindowBouncer.Delay.ToString());
            }
        }

        public Fiches() : base(nameof(Fiches))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.StartBouncing, StartBouncingHandler },  
                { (int)ProgramOptions.StopBouncing, StopBouncingHandler },  
                { (int)ProgramFields.SpeedX + 3, InputSpeedXHandler},
                { (int)ProgramFields.SpeedY + 3, InputSpeedYHandler},
                { (int)ProgramFields.Delay + 3, InputDelayHandler},
            };
            FdSpeedX = ConsoleWindowBouncer.dx.ToString();
            FdSpeedY = ConsoleWindowBouncer.dy.ToString();
            FdDelay = ConsoleWindowBouncer.Delay.ToString();
        }
        #endregion

        #region Logic 
        private void InputSpeedXHandler() 
            => FdSpeedX = int.Parse(IND.InputProperty("SpeedX")).ToString();
        private void InputSpeedYHandler() 
            => FdSpeedY = int.Parse(IND.InputProperty("SpeedY")).ToString();
        private void InputDelayHandler() 
            => FdDelay = int.Parse(IND.InputProperty("Delay")).ToString();

        private void StartBouncingHandler()
        {
            ConsoleWindowBouncer.StartBouncing();
        }
        private void StopBouncingHandler()
        {
            ConsoleWindowBouncer.StopBouncing();
        }
        #endregion
    }
}
/*
w w e
s e
*/
