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

Console.WriteLine($"Python books:");
PrintValues(queries.PythonBooks());

//Books over 450 pages, sorted by page number in descending order
Console.WriteLine($"Books over 450 pages, sorted by page number in descending order:");
PrintValues(queries.BooksOver450PagesSortedByPageNumberInDescendingOrder());

//First three Java books ordered by date
Console.WriteLine($"First three Java books ordered by date:");
PrintValues(queries.FirstThreeJavaBooksOrderedByDate());

// Examples using the newly renamed English methods
Console.WriteLine();
Console.WriteLine($"Count books between 200 and 500 pages: {queries.CountBooksBetween200And500Pages()}");
Console.WriteLine($"Earliest publication date: {queries.EarliestPublicationDate():d}");
Console.WriteLine($"Max page count: {queries.MaxPageCount()}");
var fewest = queries.BookWithFewestPages();
Console.WriteLine($"Book with fewest pages: {fewest?.Title} ({fewest?.PageCount})");
var recent = queries.MostRecentlyPublishedBook();
Console.WriteLine($"Most recently published book: {recent?.Title} ({recent?.PublishedDate:d})");
Console.WriteLine($"Sum pages between 0 and 500: {queries.SumOfPagesForBooksBetween0And500()}");
Console.WriteLine($"Concatenated titles after 2015: {queries.ConcatenatedTitlesAfter2015()}");
Console.WriteLine($"Average title characters: {queries.AverageTitleCharacters():F2}");
Console.WriteLine("Books after 2000 grouped by year:");
foreach (var g in queries.BooksAfter2000GroupedByYear())
{
    Console.WriteLine($"{g.Key}: {g.Count()} books");
}
Console.WriteLine("Books lookup by first letter for 'A':");
var lookup = queries.BooksLookupByFirstLetter();
foreach (var b in lookup['A'])
{
    Console.WriteLine(b.Title);
}
Console.WriteLine("Books after 2005 with >500 pages:");
PrintValues(queries.BooksAfter2005WithMoreThan500Pages());

void PrintValues(IEnumerable<Book> books)
{
    Console.WriteLine("{0,-60} {1,15} {2,15}\n", "Title", "Pages", "Published Date");
    foreach (var book in books)
    {
        Console.WriteLine("{0,-60} {1,15} {2,15}", book.Title, book.PageCount, book.PublishedDate.ToShortDateString());
    }
}