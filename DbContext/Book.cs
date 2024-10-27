
namespace ConsoleWinTasks;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedYear { get; set; }

    public int? PreviousBookId { get; set; }
    public Book? PreviousBook { get; set; }

    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public override string ToString() => Title;
}
