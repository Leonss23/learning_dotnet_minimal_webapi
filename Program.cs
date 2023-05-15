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

var books = new Books();
books.AddBook(new BookRequest("Book 1", "Author 1"));
books.AddBook(new BookRequest("Book 2", "Author 2"));
books.AddBook(new BookRequest("Book 3", "Author 3"));

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
        var book = books[id];

        if (book == null)
            return Results.NotFound("Book not found");

        return Results.Ok(book);
    }
);

app.MapPost(
    "/book",
    (BookRequest book) =>
    {
        books.AddBook(book);
        return Results.Ok(books);
    }
);

app.MapPut(
    "/book/{id}",
    (int id, BookRequest updatedBook) =>
    {
        if (!books.indexInRange(id))
            return Results.NotFound("Book not found");

        books[id] = new ListedBook(id, updatedBook);

        return Results.Ok(books);
    }
);

app.MapDelete(
    "/book/{id}",
    (int id) =>
    {
        if (!books.indexInRange(id))
            return Results.NotFound("Book not found");
        books.RemoveAt(id);
        return Results.Ok(books);
    }
);

app.Run();
