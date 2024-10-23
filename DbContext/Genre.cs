﻿
namespace ConsoleWinTasks;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}