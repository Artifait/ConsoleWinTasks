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
            Start,
        }
        public enum ProgramFields
        { 
            Max,
            Min,
        }
        private Action<int> AddNewNumber;
        public override Type? ProgramOptionsType => typeof(ProgramOptions);
        public List<int> numbers;
        public Random rand = new();
        public Timer timer = new(GenNumber);
        public void GenNumber()
        {
            int number = rand.Next(1, 10001);
            numbers.Add(number);
            AddNewNumber?.Invoke(number);

        }
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
        }
        private void OnAddNumberMin()
        {

        }
        private void OnAddNumberMax()
        {

        }
        #endregion
    }
}
