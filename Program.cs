LinqQueries queries = new LinqQueries();

//All collection
PrintValues(queries.GetAllBooks());

//Books with more than 250 pages and "in Action" in the title
PrintValues(queries.BooksWithMoreThan250PagesWithWordsInAction());

//Books after 2000
PrintValues(queries.BooksAfter2000());

void PrintValues(IEnumerable<Book> books)
{
    Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Title", "Pages", "Published Date");
    foreach (var book in books)
    {
        Console.WriteLine("{0,-60} {1,15} {2,15}", book.Title, book.PageCount, book.PublishedDate.ToShortDateString());
    }
}