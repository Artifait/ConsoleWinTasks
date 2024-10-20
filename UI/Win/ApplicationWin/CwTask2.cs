using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.UI.Win.WinTemplate;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class CwTask2 : CwTask
    {
        public override Type? ProgramOptionsType => typeof(ProgramOptions);
        public enum ProgramOptions
        {
            ShowSinglePlayerGame,
            ShowMultiplayerPlayerGame,
            ShowTopSellingGame,
            ShowLowestSellingGame,
            ShowTop3SellingGames,
            ShowBottom3SellingGames,
            Back
        }
        public override void HandleMenuOption()
        {
            Console.Clear();
            base.HandleMenuOption();
        }
        public CwTask2() : base(nameof(CwTask2))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back , BackHandler },
                { (int)ProgramOptions.ShowSinglePlayerGame , ShowSinglePlayerGame },
                { (int)ProgramOptions.ShowMultiplayerPlayerGame , ShowMultiplayerPlayerGame },
                { (int)ProgramOptions.ShowTopSellingGame, ShowTopSellingGame },
                { (int)ProgramOptions.ShowLowestSellingGame, ShowLowestSellingGame },
                { (int)ProgramOptions.ShowTop3SellingGames, ShowTop3SellingGames },
                { (int)ProgramOptions.ShowBottom3SellingGames, ShowBottom3SellingGames }
            };
        }

        #region Logic

        public void ShowSinglePlayerGame()
        {
            var res = ((GameContext)Application.db).Games
                .Where(g => g.GameMode.Contains("Single-player"))
                .ToList();

            TV.DisplayTable(res);
        }

        public void ShowMultiplayerPlayerGame()
        {
            var res = ((GameContext)Application.db).Games
                .Where(g => g.GameMode.Contains("Multiplayer"))
                .ToList();

            TV.DisplayTable(res);
        }

        public void ShowTopSellingGame()
        {
            var topGame = ((GameContext)Application.db).Games
                .OrderByDescending(g => g.CopiesSold)
                .FirstOrDefault();

            TV.DisplayTable(new List<Game> { topGame });
        }

        public void ShowLowestSellingGame()
        {
            var lowestGame = ((GameContext)Application.db).Games
                .OrderBy(g => g.CopiesSold)
                .FirstOrDefault();

            TV.DisplayTable(new List<Game> { lowestGame });
        }

        public void ShowTop3SellingGames()
        {
            var top3Games = ((GameContext)Application.db).Games
                .OrderByDescending(g => g.CopiesSold)
                .Take(3)
                .ToList();

            TV.DisplayTable(top3Games);
        }

        public void ShowBottom3SellingGames()
        {
            var bottom3Games = ((GameContext)Application.db).Games
                .OrderBy(g => g.CopiesSold)
                .Take(3)
                .ToList();

            TV.DisplayTable(bottom3Games);
        }

        #endregion
    }
}
