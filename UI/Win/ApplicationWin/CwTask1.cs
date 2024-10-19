
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
        { (int)ProgramOptions.FindOfName , () => {
            string name = IND.InputProperty("Название Игры");
            TV.DisplayTable(FindGamesByProperty(g => g.Name, name));
        }},
        { (int)ProgramOptions.FindOfStudio , () => {
            string studio = IND.InputProperty("Название Студии");
            TV.DisplayTable(FindGamesByProperty(g => g.Studio, studio));
        }},
        { (int)ProgramOptions.FindOfStuleGame , () => {
            string stule = IND.InputProperty("Стиль Игры");
            TV.DisplayTable(FindGamesByProperty(g => g.StuleGame, stule));
        }},
        { (int)ProgramOptions.FindOfReleaseDate , () => {
            DateOnly date = IND.InputDateTime("Релиза");
            TV.DisplayTable(FindGamesByProperty(g => g.ReleaseDate, date));
        }},
        { (int)ProgramOptions.FindOfMultipleFields , () => {
            string studio = IND.InputProperty("Название Студии");
            string game = IND.InputProperty("Название Игры");

            var filters = new Dictionary<Expression<Func<Game, object>>, object>
            {
                { g => g.Studio, studio },
                { g => g.Name, game }
            };

            TV.DisplayTable(FindGamesByProperties(filters));
        }},
        { (int)ProgramOptions.Back , BackHandler },
    };
        }


        #region Logic
        public List<Game> FindGamesByProperty<TValue>(Expression<Func<Game, TValue>> propertyExpression, TValue value)
        {
            var _context = (GameContext)Application.db;
            return _context.Games.Where(g => EF.Property<TValue>(g, (propertyExpression.Body as MemberExpression).Member.Name).Equals(value)).ToList();
        }

        public List<Game> FindGamesByProperties(Dictionary<Expression<Func<Game, object>>, object> propertyValuePairs)
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