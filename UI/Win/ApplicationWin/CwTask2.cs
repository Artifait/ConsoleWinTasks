using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.UI.Win.WinTemplate;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class CwTask2 : CwTask
    {
        public override Type? ProgramOptionsType => typeof(ProgramOptions);
        private GameContext db;

        public enum ProgramOptions
        {
            Back,
            DisplaySinglePlayerGameCount,
            DisplayMultiPlayerGameCount,
            ShowGameWithMaxSalesByStyle,
            DisplayTop5BestSellingGamesByStyle,
            DisplayTop5WorstSellingGamesByStyle,
            DisplayGameFullInfo,
            DisplayStudioNameAndTopStyleByGameCount,
        }

        public CwTask2() : base(nameof(CwTask2))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler },
                { (int)ProgramOptions.DisplaySinglePlayerGameCount, DisplaySinglePlayerGameCountHandler },
                { (int)ProgramOptions.DisplayMultiPlayerGameCount, DisplayMultiPlayerGameCountHandler },
                { (int)ProgramOptions.ShowGameWithMaxSalesByStyle, ShowGameWithMaxSalesByStyleHandler },
                { (int)ProgramOptions.DisplayTop5BestSellingGamesByStyle, DisplayTop5BestSellingGamesByStyleHandler },
                { (int)ProgramOptions.DisplayTop5WorstSellingGamesByStyle, DisplayTop5WorstSellingGamesByStyleHandler },
                { (int)ProgramOptions.DisplayGameFullInfo, DisplayGameFullInfoHandler },
                { (int)ProgramOptions.DisplayStudioNameAndTopStyleByGameCount, DisplayStudioNameAndTopStyleByGameCountHandler },
            };
            db = (GameContext)Application.db;
        }

        #region Logic
        private void DisplaySinglePlayerGameCountHandler()
        {
            var count = db.Games.Count(g => g.GameMode == "SinglePlayer");
            TV.DisplayTable(new List<int> { count });
        }

        private void DisplayMultiPlayerGameCountHandler()
        {
            var count = db.Games.Count(g => g.GameMode == "MultiPlayer");
            TV.DisplayTable(new List<int> { count });
        }

        private void ShowGameWithMaxSalesByStyleHandler()
        {
            Console.Write("Введите стиль игры: ");
            var style = Console.ReadLine();
            var game = db.Games
                         .Where(g => g.StuleGame == style)
                         .OrderByDescending(g => g.CopiesSold)
                         .FirstOrDefault();

            if (game != null)
            {
                TV.DisplayTable(new List<Game> { game });
            }
            else
            {
                Console.WriteLine("Игра с данным стилем не найдена.");
            }
        }

        private void DisplayTop5BestSellingGamesByStyleHandler()
        {
            Console.Write("Введите стиль игры: ");
            var style = Console.ReadLine();
            var games = db.Games
                          .Where(g => g.StuleGame == style)
                          .OrderByDescending(g => g.CopiesSold)
                          .Take(5)
                          .ToList();

            TV.DisplayTable(games);
        }

        private void DisplayTop5WorstSellingGamesByStyleHandler()
        {
            Console.Write("Введите стиль игры: ");
            var style = Console.ReadLine();
            var games = db.Games
                          .Where(g => g.StuleGame == style)
                          .OrderBy(g => g.CopiesSold)
                          .Take(5)
                          .ToList();

            TV.DisplayTable(games);
        }

        private void DisplayGameFullInfoHandler()
        {
            Console.Write("Введите ID игры: ");
            if (int.TryParse(Console.ReadLine(), out int gameId))
            {
                var game = db.Games
                             .Where(g => g.Id == gameId)
                             .Select(g => new
                             {
                                 g.Id,
                                 g.Name,
                                 g.StuleGame,
                                 StudioName = g.Studio.Name,
                                 g.ReleaseDate,
                                 g.GameMode,
                                 g.CopiesSold
                             })
                             .FirstOrDefault();

                if (game != null)
                {
                    TV.DisplayTable(new List<object> { game });
                }
                else
                {
                    Console.WriteLine("Игра с данным ID не найдена.");
                }
            }
        }

        private void DisplayStudioNameAndTopStyleByGameCountHandler()
        {
            var result = db.Studios
                           .Select(s => new
                           {
                               StudioName = s.Name,
                               TopStyle = s.Games
                                           .GroupBy(g => g.StuleGame)
                                           .OrderByDescending(g => g.Count())
                                           .Select(g => g.Key)
                                           .FirstOrDefault()
                           })
                           .ToList();

            TV.DisplayTable(result);
        }
        #endregion
    }
}
