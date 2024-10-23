using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Task1 : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Test1,
            Test2,
             InputLogin,
            InputPassword,
            InputBirthDate,
            InputAge,
            InputSalary,
        }
        public enum ProgramFields
        {
            Login,
            Password,
            BirthDate,
            Age,
            Salary,
        } 

        public override Type? ProgramFieldsType => typeof(ProgramFields);        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public string FdLogin
        {
            get => WindowDisplay.GetOrCreateField("Login");
            set => WindowDisplay.AddOrUpdateField("Login", value);
        } 
        public string FdPassword
        {
            get => WindowDisplay.GetOrCreateField("Password");
            set => WindowDisplay.AddOrUpdateField("Password", value);
        } 
        public string FdBirthDate
        {
            get => WindowDisplay.GetOrCreateField("BirthDate", DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd"));
            set => WindowDisplay.AddOrUpdateField("BirthDate", value);
        } 
        public string FdAge
        {
            get => WindowDisplay.GetOrCreateField("Age", "0");
            set => WindowDisplay.AddOrUpdateField("Age", value);
        } 
        public string FdSalary
        {
            get => WindowDisplay.GetOrCreateField("Salary", "0");
            set => WindowDisplay.AddOrUpdateField("Salary", value);
        } 
 
        public Task1() : base(nameof(Task1))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.Test1, Test1Handler },  
                { (int)ProgramOptions.Test2, Test2Handler },  
                { (int)ProgramFields.Login + 3, InputLoginHandler},
                { (int)ProgramFields.Password + 3, InputPasswordHandler},
                { (int)ProgramFields.BirthDate + 3, InputBirthDateHandler},
                { (int)ProgramFields.Age + 3, InputAgeHandler},
                { (int)ProgramFields.Salary + 3, InputSalaryHandler},
            };
        }

        public override void HandleMenuOption()
        {
            try { base.HandleMenuOption(); }
            catch(Exception ex) { WindowsHandler.AddErroreWindow([ex.Message]); }
        }
        #endregion

        #region Logic 
        private void InputLoginHandler() 
            => FdLogin = IND.InputProperty("Login");
        private void InputPasswordHandler() 
            => FdPassword = IND.InputProperty("Password");
        private void InputBirthDateHandler() 
            => FdBirthDate = IND.InputDateTime("BirthDate").ToString("yyyy-MM-dd");
        private void InputAgeHandler() 
            => FdAge = int.Parse(IND.InputProperty("Age")).ToString();
        private void InputSalaryHandler() 
            => FdSalary = float.Parse(IND.InputProperty("Salary")).ToString();
 
        private void Test1Handler()
        {
            Console.WriteLine("Test1Handler");
        }
        private void Test2Handler()
        {
            Console.WriteLine("Test2Handler");
        }
        #endregion
    }
}