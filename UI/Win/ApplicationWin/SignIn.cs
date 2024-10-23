using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.AppLogic;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class SignIn : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            SignIn, 
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
 
        public SignIn() : base(nameof(SignIn))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.SignIn, SignInHandler },  
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
 
        private void SignInHandler()
        {
            string resultMessage = "Не верный логин или пароль";
            string login = FdLogin.Trim();
            string password = FdPassword.Trim();
            var db = Application.db;
            User? user = db.Users.Where(g => g.Username == login).FirstOrDefault();
            if (user == null)
                goto ShowResult;

            if (!PasswordManager.VerifyHashedPassword(user.HashPassword, password))
                goto ShowResult;

            resultMessage = "Успешный Вход";
            WindowsHandler.GetWindow<MainWindow>().FdLogin = login;
            WindowsHandler.AddWindow<MainWindow>();
        ShowResult:
            WindowsHandler.AddInfoWindow([resultMessage]);

        }
        #endregion
    }
}
