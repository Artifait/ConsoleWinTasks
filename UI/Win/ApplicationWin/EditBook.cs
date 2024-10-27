using ConsoleWinTasks.UI.Win.WinTemplate;
using ConsoleWinTasks.UI.ConsoleFrameWork;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleWinTasks.UI.Win.ApplicationWin
{
    public class EditBook : CwTask
    {
        #region GeneratedСode
       public enum ProgramOptions 
        { 
            Back, 
            ChooseEditBook,
            SaveChange,
 
            InputAuthor,
            InputGenre,
            InputPublisher,
            InputDiscount,
            InputPreviousBook,
            InputPublishedYear,
            InputTitle,
            InputCostPrice,
            InputSalePrice,
            InputPageCount,
        }
        public enum ProgramFields
        {
            Author,
            Genre,
            Publisher,
            Discount,
            PreviousBook,
            PublishedYear,
            Title,
            CostPrice,
            SalePrice,
            PageCount,
        } 

        public override Type? ProgramFieldsType => typeof(ProgramFields);
        public override Type? ProgramOptionsType => typeof(ProgramOptions);

        public string FdAuthor
        {
            get => WindowDisplay.GetOrCreateField("Author");
            set => WindowDisplay.AddOrUpdateField("Author", value);
        } 
        public string FdGenre
        {
            get => WindowDisplay.GetOrCreateField("Genre");
            set => WindowDisplay.AddOrUpdateField("Genre", value);
        } 
        public string FdPublisher
        {
            get => WindowDisplay.GetOrCreateField("Publisher");
            set => WindowDisplay.AddOrUpdateField("Publisher", value);
        } 
        public string FdDiscount
        {
            get => WindowDisplay.GetOrCreateField("Discount");
            set => WindowDisplay.AddOrUpdateField("Discount", value);
        } 
        public string FdPreviousBook
        {
            get => WindowDisplay.GetOrCreateField("PreviousBook");
            set => WindowDisplay.AddOrUpdateField("PreviousBook", value);
        } 
        public string FdPublishedYear
        {
            get => WindowDisplay.GetOrCreateField("PublishedYear", DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd"));
            set => WindowDisplay.AddOrUpdateField("PublishedYear", value);
        } 
        public string FdTitle
        {
            get => WindowDisplay.GetOrCreateField("Title");
            set => WindowDisplay.AddOrUpdateField("Title", value);
        } 
        public string FdCostPrice
        {
            get => WindowDisplay.GetOrCreateField("CostPrice", "0");
            set => WindowDisplay.AddOrUpdateField("CostPrice", value);
        } 
        public string FdSalePrice
        {
            get => WindowDisplay.GetOrCreateField("SalePrice", "0");
            set => WindowDisplay.AddOrUpdateField("SalePrice", value);
        } 
        public string FdPageCount
        {
            get => WindowDisplay.GetOrCreateField("PageCount", "0");
            set => WindowDisplay.AddOrUpdateField("PageCount", value);
        } 
 
        public EditBook() : base(nameof(EditBook))
        {
            MenuHandlers = new()
            {
                { (int)ProgramOptions.Back, BackHandler }, 
                { (int)ProgramOptions.ChooseEditBook, ChooseEditBookHandler },  
                { (int)ProgramOptions.SaveChange, SaveChangeHandler },  
                { (int)ProgramFields.Author + 3, InputAuthorHandler},
                { (int)ProgramFields.Genre + 3, InputGenreHandler},
                { (int)ProgramFields.Publisher + 3, InputPublisherHandler},
                { (int)ProgramFields.Discount + 3, InputDiscountHandler},
                { (int)ProgramFields.PreviousBook + 3, InputPreviousBookHandler},
                { (int)ProgramFields.PublishedYear + 3, InputPublishedYearHandler},
                { (int)ProgramFields.Title + 3, InputTitleHandler},
                { (int)ProgramFields.CostPrice + 3, InputCostPriceHandler},
                { (int)ProgramFields.SalePrice + 3, InputSalePriceHandler},
                { (int)ProgramFields.PageCount + 3, InputPageCountHandler},
            };
        }
        #endregion

        #region Logic 
        private Book editBook;
        private void InitFieldsOfBook(Book book)
        {
            FdPublishedYear = book.PublishedYear.ToString("yyyy-MM-dd");
            FdPreviousBook = book.PreviousBook?.ToString() ?? "Нету";
            FdDiscount = book.Discount?.ToString() ?? "Нету";
            FdCostPrice = book.CostPrice.ToString();
            FdPageCount = book.PageCount.ToString();
            FdAuthor = book.Author.ToString();
            int idP = book.PublisherId;
            FdPublisher = book.Publisher.ToString();
            FdSalePrice = book.SalePrice.ToString();
            FdGenre = book.Genre.ToString();
            FdTitle = book.Title;
        }
        private void InputAuthorHandler() 
        {
            if(editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначало выберите книгу"]);
                return;
            }
            editBook.Author = MainWindow.SelectEntity(windowDisplay, "Автор", Application.db.Authors.ToList(),
                g => new { g.Id, g.FirstName, g.LastName, g.MiddleName });
            editBook.AuthorId = editBook.Author.Id;

            FdAuthor = editBook.Author.ToString();
        }
        private void InputGenreHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            editBook.Genre = MainWindow.SelectEntity(windowDisplay, "Жанр", Application.db.Genres.ToList(),
                g => new { g.Id, g.Name });
            editBook.GenreId = editBook.Genre.Id;

            FdGenre = editBook.Genre.ToString();
        }

        private void InputPublisherHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            editBook.Publisher = MainWindow.SelectEntity(windowDisplay, "Издатель", Application.db.Publishers.ToList(),
                p => new { p.Id, p.Name });
            editBook.PublisherId = editBook.Publisher.Id;

            FdPublisher = editBook.Publisher.ToString();
        }

        private void InputDiscountHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            editBook.Discount = MainWindow.SelectEntity(windowDisplay, "Скидка", Application.db.Discounts.ToList(),
                d => new { d.Id, d.Name, d.Multiplier });
            editBook.DiscountId = editBook.Discount?.Id;

            FdDiscount = editBook.Discount?.ToString() ?? "Нету";
        }

        private void InputPreviousBookHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }
            var prevBook = MainWindow.SelectEntity(windowDisplay, "Предыдущая книга", Application.db.Books.ToList(),
                g => new { g.Id, g.Title, g.Author, g.Genre, g.Publisher, g.PublishedYear });
            if(prevBook.Id == editBook.Id)
            {
                WindowsHandler.AddInfoWindow(["Операция отменена\nВы ссылаетесь на текущую книгу"]);
                return;
            }
            editBook.PreviousBook = prevBook;
            editBook.PreviousBookId = editBook.PreviousBook?.Id;

            FdPreviousBook = editBook.PreviousBook?.ToString() ?? "Нету";
        }
        private void InputPublishedYearHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            FdPublishedYear = (editBook.PublishedYear = IND.InputDateTime("PublishedYear")).ToString("yyyy-MM-dd");
        }
        private void InputTitleHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            FdTitle = editBook.Title = IND.InputProperty("Title").Trim();
        }
        private void InputCostPriceHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            FdCostPrice = (editBook.CostPrice = int.Parse(IND.InputProperty("CostPrice"))).ToString();
        }
        private void InputSalePriceHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            FdSalePrice = (editBook.SalePrice = int.Parse(IND.InputProperty("SalePrice"))).ToString();
        }
        private void InputPageCountHandler()
        {
            if (editBook == null)
            {
                WindowsHandler.AddInfoWindow(["Сначала выберите книгу"]);
                return;
            }

            FdPageCount = (editBook.PageCount = int.Parse(IND.InputProperty("PageCount"))).ToString();
        }
 
        private void ChooseEditBookHandler()
        {
            var books = Application.db.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.PreviousBook)
                .Include(b => b.Discount);
            if(!books.Any())
            {
                WindowsHandler.AddInfoWindow(["Нету книг для редактирования"]);
                return;
            }
            TV.DisplayTable(books.Select(g => new { g.Id, g.Title, g.Author, g.Genre, g.Publisher, g.PublishedYear }).ToList());
            int id = int.Parse(IND.InputProperty("Id Книги для Редактирования"));
            editBook = books.FirstOrDefault(b => b.Id == id);
            if (editBook != null)
                InitFieldsOfBook(editBook);
            else
                WindowsHandler.AddInfoWindow(["Нету подходящей книги"]);
            
        }
        private void SaveChangeHandler()
        {
            Application.db.SaveChanges();
            DropEditBook();
        }
        private void DropEditBook()
        {
            WindowDisplay.ClearValuesFields();
            editBook = null!;
            Console.Clear();
        }
        #endregion
    }
}
