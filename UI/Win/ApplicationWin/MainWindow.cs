using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using Microsoft.EntityFrameworkCore;

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
        private T SelectEntity<T>(string fieldName, List<T> list, Func<T, object> selector)
        {
            TV.DisplayTable(list.Select(selector).ToList());
            int id = int.Parse(IND.InputProperty($"Id {fieldName}"));
            var entity = list.FirstOrDefault(g => (int)typeof(T).GetProperty("Id")!.GetValue(g, null)! == id)
                ?? throw new Exception("Не найдена запись");
            WindowDisplay.AddOrUpdateField(fieldName, entity?.ToString() ?? string.Empty);
            return entity;
        }
        private void CreateBookHandler()
        {
            Book book = new() { };
            ExtraFields = true;

            book.Author = SelectEntity("Автор", Application.db.Authors.ToList(),
                g => new { g.Id, g.FirstName, g.LastName, g.MiddleName });
            book.AuthorId = book.Author.Id;
            ToStartWritePos();

            book.Genre = SelectEntity("Жанр", Application.db.Genres.ToList(),
                g => new { g.Id, g.Name });
            book.GenreId = book.Genre.Id;
            ToStartWritePos();

            book.Publisher = SelectEntity("Издательство", Application.db.Publishers.ToList(),
                g => new { g.Id, g.Name });
            book.PublisherId = book.Publisher.Id;
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
                book.PreviousBook = SelectEntity("Прошлая Часть", Application.db.Books.ToList(),
                g => new { g.Id, g.Title, g.Author });
                book.PreviousBookId = book.PreviousBook.Id;
                ToStartWritePos();
            }
            Console.WriteLine("На книгу сейчас есть скидка(e - да, иначе - нет)");
            if (WindowTools.IsKeySelect(Char.ToLower(Console.ReadKey().KeyChar)))
            {
                book.Discount = SelectEntity("Скидка", Application.db.Discounts.ToList(),
                g => new { g.Id, g.Name, g.Multiplier });
                book.DiscountId = book.Discount.Id;
                ToStartWritePos();
            }

            Application.db.Books.Add(book);
            Application.db.SaveChanges();
        }


        private void EditBookHandler()
        {
            Application.db.Books.Include(b => b.Author);
            Application.db.Books.Include(b => b.Genre);
            Application.db.Books.Include(b => b.Publisher);
            Application.db.Books.Include(b => b.PreviousBook);

            TV.DisplayTable(Application.db.Books.Select(g => new { g.Id, g.Title, Author = g.Author.ToString() }).ToList());
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
