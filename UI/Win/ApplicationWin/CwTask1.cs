
using ConsoleWinTasks.UI.ConsoleFrameWork;
using ConsoleWinTasks.UI.Win.WinTemplate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class CwTask1 : CwTask
    {
        public override Type? ProgramOptionsType => typeof(ProgramOptions);
        public enum ProgramOptions { FindOfName, FindOfStudio, FindOfStuleGame, FindOfReleaseDate, FindOfMultipleFields, Back }

        public CwTask1() : base(nameof(CwTask1))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.FindOfName, () => TV.DisplayTable(FindOfName(IND.InputProperty("Название Игры"))) },
                { (int)ProgramOptions.FindOfStudio, () => TV.DisplayTable(FindOfStudio(IND.InputProperty("Название Студии"))) },
                { (int)ProgramOptions.FindOfStuleGame, () => TV.DisplayTable(FindOfStuleGame(IND.InputProperty("Стиль Игры"))) },
                { (int)ProgramOptions.FindOfReleaseDate, () => TV.DisplayTable(FindOfReleaseDate(IND.InputDateTime("Релиза"))) },
                { (int)ProgramOptions.FindOfMultipleFields , () => 
                    TV.DisplayTable(FindOfMultipleFields(IND.InputProperty("Название Студии"), IND.InputProperty("Название Игры")))
                },
                { (int)ProgramOptions.Back , BackHandler },
            };
        }


        #region Logic

        public static List<Game> FindOfName(string name)
            => FindGamesByProperty(g => g.Name, name);

        public static List<Game> FindOfStudio(string studio)
            => FindGamesByProperty(g => g.Studio, studio);

        public static List<Game> FindOfStuleGame(string stule)
            => FindGamesByProperty(g => g.StuleGame, stule);

        public static List<Game> FindOfReleaseDate(DateOnly date)
            => FindGamesByProperty(g => g.ReleaseDate, date);

        public static List<Game> FindOfMultipleFields(string studio, string game)
        {
            var filters = new Dictionary<Expression<Func<Game, object>>, object>
            {
                { g => g.Studio, studio },
                { g => g.Name, game }
            };
            return FindGamesByProperties(filters);
        }

        public static List<Game> FindGamesByProperty<TValue>(Expression<Func<Game, TValue>> propertyExpression, TValue value)
        {
            var _context = (GameContext)Application.db;
            return _context.Games.Where(g => EF.Property<TValue>(g, (propertyExpression.Body as MemberExpression).Member.Name).Equals(value)).ToList();
        }

        public static List<Game> FindGamesByProperties(Dictionary<Expression<Func<Game, object>>, object> propertyValuePairs)
        {
            var _context = (GameContext)Application.db;
            IQueryable<Game> query = _context.Games;

            foreach (var pair in propertyValuePairs)
            {
                var propertyExpression = pair.Key;
                var value = pair.Value;

                query = query.Where(g => EF.Property<object>(g, (propertyExpression.Body as MemberExpression).Member.Name).Equals(value));
            }

            return query.ToList();
        }

        #endregion
    }
}