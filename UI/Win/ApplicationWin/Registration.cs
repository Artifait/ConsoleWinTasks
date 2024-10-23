using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class Registration : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            Registration, 
            InputLogin,
            InputPassword,
        }
        public enum ProgramFields
        {
            Login,
            Password,
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
 
        public Registration() : base(nameof(Registration))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.Registration, RegistrationHandler },  
                { (int)ProgramFields.Login + 2, InputLoginHandler},
                { (int)ProgramFields.Password + 2, InputPasswordHandler},
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
 
        private void RegistrationHandler()
        {
            Console.WriteLine("RegistrationHandler");
        }
        #endregion
    }
}
