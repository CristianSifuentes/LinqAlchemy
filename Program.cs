LinqQueries queries = new LinqQueries();

//All collection
PrintValues(queries.GetAllBooks());

//Books with more than 250 pages and "in Action" in the title
PrintValues(queries.BooksWithMoreThan250PagesWithWordsInAction());

//Books after 2000
PrintValues(queries.BooksAfter2000());

// Dynamic text search across all string properties in Book
Console.WriteLine();
Console.WriteLine("Search results for term: \"android\"");
PrintValues(queries.SearchBooks("android"));

PrintValues(queries.SearchBooks("java"));
PrintValues(queries.SearchBooks("c#"));

//All books have Status
Console.WriteLine($"All books have status: {queries.AllBooksHaveStatus()}");
//Any book published in 2005
Console.WriteLine($"Any book published in 2005: {queries.IfAnyBookWasPublished2005()}");


void PrintValues(IEnumerable<Book> books)
{
    Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Title", "Pages", "Published Date");
    foreach (var book in books)
    {
        Console.WriteLine("{0,-60} {1,15} {2,15}", book.Title, book.PageCount, book.PublishedDate.ToShortDateString());
    }
}