public class Books : List<ListedBook>
{
    public bool indexInRange(int index)
    {
        return (index >= 0) && (index < Count);
    }

    public void AddBook(BookRequest book)
    {
        this.Add(new ListedBook(this.Count, book));
    }
}

public class ListedBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public ListedBook(int id, BookRequest book)
    {
        Id = id;
        Title = book.Title;
        Author = book.Author;
    }
}

public class BookRequest
{
    public string Title { get; set; }
    public string Author { get; set; }

    public BookRequest(string title, string author)
    {
        Title = title;
        Author = author;
    }
}
