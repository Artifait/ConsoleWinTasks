﻿
namespace ConsoleWinTasks;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Multiplier { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();

    public override string ToString() => $"Discount name: {Name}\nPrice multiplier: {Multiplier}";
}   