using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class MainWindow : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            CreateBook,
            EditBook,
            DeleteBook,
            ToBookStoreInformation,
 
        }
        public enum ProgramFields
        {
            Login,
        } 

        public override Type? ProgramFieldsType => typeof(ProgramFields);
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public bool ExtraFields = false;

        public string FdLogin
        {
            get => WindowDisplay.GetOrCreateField("Login");
            set => WindowDisplay.AddOrUpdateField("Login", value);
        } 
 
        public MainWindow() : base(nameof(MainWindow))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.CreateBook, CreateBookHandler },  
                { (int)ProgramOptions.EditBook, EditBookHandler },  
                { (int)ProgramOptions.DeleteBook, DeleteBookHandler },  
                { (int)ProgramOptions.ToBookStoreInformation, ToBookStoreInformationHandler },  
            };
        }

        public override void HandleMenuOption()
        {
            try { base.HandleMenuOption(); } catch (Exception ex) { WindowsHandler.AddErroreWindow([ex.Message]); }
            DeleteExtraFields();
        }
        #endregion

        #region Logic 
        private void DeleteExtraFields()
        {
            if (!ExtraFields) return;

            var validKeys = Enum.GetNames(typeof(ProgramFields));
            var keysToRemove = WindowDisplay.Fields.Keys.Where(key => !validKeys.Contains(key)).ToList();
            foreach (var key in keysToRemove)
                WindowDisplay.Fields.Remove(key);
            windowDisplay.UpdateCanvas();
            ExtraFields = false;
            Console.Clear();
        }

        private void ToStartWritePos()
        {
            Console.Clear();
            WindowDisplay.Show();
            Console.CursorTop = SizeY;
            Console.CursorLeft = 0;
        }
        private int SelectEntity<T>(string fieldName, List<T> list, Func<T, object> selector)
        {
            TV.DisplayTable(list.Select(selector).ToList());
            int id = int.Parse(IND.InputProperty($"Id {fieldName}"));
            var entity = list.FirstOrDefault(g => (int)typeof(T).GetProperty("Id")!.GetValue(g, null)! == id)
                ?? throw new Exception("Не найдена запись");
            WindowDisplay.AddOrUpdateField(fieldName, entity?.ToString() ?? string.Empty);
            return id;
        }
        private void CreateBookHandler()
        {
            Book book = new() { };
            ExtraFields = true;

            book.AuthorId = SelectEntity("Автор", Application.db.Authors.ToList(),
                g => new { g.Id, g.FirstName, g.LastName, g.MiddleName });
            ToStartWritePos();

            book.GenreId = SelectEntity("Жанр", Application.db.Genres.ToList(),
                g => new { g.Id, g.Name });
            ToStartWritePos();

            book.PublisherId = SelectEntity("Издательство", Application.db.Publishers.ToList(),
                g => new { g.Id, g.Name });
            ToStartWritePos();

            book.Title = IND.InputProperty("Название").Trim();
            WindowDisplay.AddOrUpdateField("Название", book.Title);

            book.CostPrice = int.Parse(IND.InputProperty("Себестоимость"));
            WindowDisplay.AddOrUpdateField("Себестоимость", book.CostPrice.ToString());

            book.SalePrice = int.Parse(IND.InputProperty("Цену"));
            WindowDisplay.AddOrUpdateField("Цена", book.SalePrice.ToString());

            book.PageCount = int.Parse(IND.InputProperty("Кол-во страниц"));
            WindowDisplay.AddOrUpdateField("Кол-во страниц", book.PageCount.ToString());
            ToStartWritePos();

            book.PublishedYear = IND.InputDateTime("Публикации");
            WindowDisplay.AddOrUpdateField("Дата Публикации", book.PublishedYear.ToString("yyyy-MM-dd"));
            ToStartWritePos();

            Console.WriteLine("У книги есть прошлая часть(e - да, иначе - нет)");
            if(WindowTools.IsKeySelect(Char.ToLower(Console.ReadKey().KeyChar)))
            {
                book.PreviousBookId = SelectEntity("Прошлая Часть", Application.db.Books.ToList(),
                g => new { g.Id, g.Title, g.Author });
            }
            ToStartWritePos();
            Console.ReadLine();
            
            Application.db.Books.Add(book);
            Application.db.SaveChanges();
            ToStartWritePos();
            TV.DisplayTable(Application.db.Books.ToList());
        }


        private void EditBookHandler()
        {
            Console.WriteLine("EditBookHandler");
        }
        private void DeleteBookHandler()
        {
            Console.WriteLine("DeleteBookHandler");
        }
        private void ToBookStoreInformationHandler()
        {
            Console.WriteLine("ToBookStoreInformationHandler");
        }
        #endregion
    }
}
