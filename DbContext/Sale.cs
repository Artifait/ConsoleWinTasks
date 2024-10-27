

namespace ConsoleWinTasks;

public class Sale
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int DiscountId { get; set; }
    public Discount Discount { get; set; } = null!;
}
