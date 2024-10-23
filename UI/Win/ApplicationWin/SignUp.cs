using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.AppLogic;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class SignUp : CwTask
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
 
        public SignUp() : base(nameof(SignUp))
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
            Console.WriteLine("Вот как система видит ваш логин и пароль.");
            Console.WriteLine($"Логин: {FdLogin.Trim()}");
            Console.WriteLine($"Пароль: {FdPassword.Trim()}\n");
            Console.Write("Продолжить регистрацию?(enter - да, иначе - нет): ");
            string resultMessage = "Регистрация отменена";
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                string login = FdLogin.Trim();
                string password = FdPassword.Trim();
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    resultMessage = "Вы не ввели логин или пароль";
                    goto ShowResult;
                }
                string hash = PasswordManager.HashPassword(password);
                var db = Application.db;
                User? user = db.Users.Where(g => g.Username == login).FirstOrDefault();
                if (user != null)
                {
                    resultMessage = "Логин уже занят";
                    goto ShowResult;
                }
                
                db.Users.Add(new() { HashPassword = hash, Username = login});
                db.SaveChanges();
                resultMessage = "Регистрация завершена успешно.";
            }
            var Fields = new List<(string FieldName, string Type, bool GenerateHandler)>
            {
                ("Login", "string", false),
            };
        ShowResult:
            WindowsHandler.AddInfoWindow([resultMessage]);
        }
        #endregion
    }
}
