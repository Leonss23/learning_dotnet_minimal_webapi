var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book
    {
        Id = 1,
        Title = "Book 1",
        Author = "Author 1"
    },
    new Book
    {
        Id = 2,
        Title = "Book 2",
        Author = "Author 2"
    },
    new Book
    {
        Id = 3,
        Title = "Book 3",
        Author = "Author 3"
    },
};

app.MapGet(
    "/book",
    () =>
    {
        if (books == null)
            return Results.NotFound("There are no books.");

        return Results.Ok(books);
    }
);

app.MapGet(
    "/book/{id}",
    (int id) =>
    {
        var book = books.Find(book => book.Id == id);

        if (book == null)
            return Results.NotFound("Book not found");

        return Results.Ok(book);
    }
);

app.MapPost(
    "/book",
    (Book book) =>
    {
        books.Add(book);
        return Results.Ok(books);
    }
);

app.MapPut(
    "/book",
    (Book updatedBook) =>
    {
        var foundBook = books.Find(book => book.Id == updatedBook.Id);
        if (foundBook == null)
            return Results.NotFound("Book not found");

        books.Remove(foundBook);
        books.Add(updatedBook);

        return Results.Ok(books);
    }
);

app.MapDelete(
    "/book/{id}",
    (int id) =>
    {
        books.RemoveAll(book => book.Id == id);
    }
);

app.Run();
