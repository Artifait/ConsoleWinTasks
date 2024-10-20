
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.UI.Win.WinTemplate;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class CwTask3 : CwTask
    {
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public enum ProgramOptions 
        { 
            CreateGame,
            UpdateGame,
            DeleteGame,
            Back
        }
        public override void HandleMenuOption()
        {
            Console.Clear();
            base.HandleMenuOption();

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
            GameContext db = (GameContext)Application.db;
            string name = IND.InputProperty("Название игры");
            if (CwTask1.FindOfName(name).Count > 0)
            {
                Console.WriteLine("Такая игра есть, вам отказано в создании игры :)");
                return;
            }

            Game game = new()
            {
                CopiesSold = int.Parse(IND.InputProperty("На скоко продали")),
                ReleaseDate = IND.InputDateTime("Релиза"),
                Studio = IND.InputProperty("Студию"),
                StuleGame = IND.InputProperty("Стиль Игры"),
                GameMode = GetGameMode(),
                Name = name,
            };

            db.Games.Add(game);
            db.SaveChanges();
            ShowResult(db);
        }
        private void UpdateGameHandler()
        {
            GameContext db = (GameContext)Application.db;
            TV.DisplayTable(db.Games.ToList());
            int id = int.Parse(IND.InputProperty("Id игры для редактирования"));
            Game game = db.Games.FirstOrDefault(x => x.Id == id);
            if (game != null)
            {
                game.CopiesSold = int.Parse(IND.InputProperty("На скоко продали"));
                game.ReleaseDate = IND.InputDateTime("Релиза");
                game.Studio = IND.InputProperty("Студию");
                game.StuleGame = IND.InputProperty("Стиль Игры");
                game.GameMode = GetGameMode();
                string name;
                inputName:
                name = IND.InputProperty("Название игры");
                if (CwTask1.FindOfName(name).Count > 0)
                {
                    Console.WriteLine("Такая игра есть, введите заного :)");
                    goto inputName;
                }
                game.Name = name;
                db.SaveChanges();
                ShowResult(db);
            }
            else
            {
                WindowsHandler.AddInfoWindow(["Нет такой игры"]);
            }
        }
        private void DeleteGameHandler()
        {
            GameContext db = (GameContext)Application.db;
            TV.DisplayTable(db.Games.ToList());
            int id = int.Parse(IND.InputProperty("Id игры для удаления"));
            Game game = db.Games.FirstOrDefault(x => x.Id == id);
            if (game != null)
            {
                db.Games.Remove(game);
                db.SaveChanges();
                ShowResult(db);
            }
            else
            {
                WindowsHandler.AddInfoWindow(["Нет такой игры"]);
            }
        }

        private string GetGameMode()
        {
            string mode = IND.InputProperty("1 - Single, 2 - Multiple, 3 - s/m");
            switch (mode)
            {
                case "1": mode = "Single-player"; break;
                case "2": mode = "Multiplayer"; break;
                case "3": mode = "Single-player/Multiplayer"; break;
                default: break;
            }
            return mode;
        }
        private void ShowResult(GameContext db)
        {
            Console.Clear();
            Console.CursorTop = SizeY;
            TV.DisplayTable(db.Games.ToList());
            Console.CursorTop = 0;
        }
        #endregion
    }
}
