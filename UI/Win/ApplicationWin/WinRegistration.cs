
using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class WinRegistration : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Enter, 
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
        
        public override Type? ProgramFieldsType => typeof(ProgramFields);
        public override Type? ProgramOptionsType => typeof(ProgramOptions);
        
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
            get => WindowDisplay.GetOrCreateField("BirthDate");
            set => WindowDisplay.AddOrUpdateField("BirthDate", value);
        } 
        public string FdAge
        {
            get => WindowDisplay.GetOrCreateField("Age");
            set => WindowDisplay.AddOrUpdateField("Age", value);
        } 
        public string FdSalary
        {
            get => WindowDisplay.GetOrCreateField("Salary");
            set => WindowDisplay.AddOrUpdateField("Salary", value);
        }  
        
        public WinRegistration() : base(nameof(WinRegistration))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.Enter, EnterHandler }, 
                { (int)ProgramFields.Login + 2, InputLoginHandler},
                { (int)ProgramFields.Password + 2, InputPasswordHandler},
                { (int)ProgramFields.BirthDate + 2, InputBirthDateHandler},
                { (int)ProgramFields.Age + 2, InputAgeHandler},
                { (int)ProgramFields.Salary + 2, InputSalaryHandler},
            };
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
        private void EnterHandler()
        {
            Console.WriteLine("EnterHandler");
        }
        
        #endregion
    }
}
