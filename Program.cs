LinqQueries queries = new LinqQueries();

PrintValues(queries.GetAllBooks());

void PrintValues(IEnumerable<Book> books)
{
    Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Title", "Pages", "Published Date");
    foreach (var book in books)
    {
        Console.WriteLine("{0,-60} {1,15} {2,15}", book.Title, book.PageCount, book.PublishedDate.ToShortDateString());
    }
}