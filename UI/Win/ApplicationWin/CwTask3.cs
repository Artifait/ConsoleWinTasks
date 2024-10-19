

using ConsoleWinTasks.UI.Win.WinTemplate;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class CwTask3 : CwTask
    {
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public enum ProgramOptions 
        { 
            Back,
            CreateGame,
            UpdateGame,
            DeleteGame,
        }

        public CwTask3() : base(nameof(CwTask3))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler },
                { (int)ProgramOptions.CreateGame, CreateGameHandler },
                { (int)ProgramOptions.UpdateGame, UpdateGameHandler },
                { (int)ProgramOptions.DeleteGame, DeleteGameHandler },
            };
        }

        #region Logic
        private void CreateGameHandler()
        {
            // Логика для CreateGame
        }
        private void UpdateGameHandler()
        {
            // Логика для UpdateGame
        }
        private void DeleteGameHandler()
        {
            // Логика для DeleteGame
        }
        #endregion
    }
}
