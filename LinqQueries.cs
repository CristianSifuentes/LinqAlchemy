public class LinqQueries
{
    private List<Book> booksCollection = new List<Book>();

    public LinqQueries()
    {
        // Try loading from the app's base directory (where dotnet publishes/copies files)
        string path = Path.Combine(AppContext.BaseDirectory, "books.json");

        // Fallback to the current working directory if not found in the base directory
        if (!File.Exists(path))
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "books.json");
        }

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            this.booksCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<Book>();
        }
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return booksCollection;
    }
	
	public IEnumerable<Book> BooksAfter2000(){

        // extension method
        // return booksCollection.Where(p => p.PublishedDate.Year > 2000);
        //query expresion 
        return from book in booksCollection
                where book.PublishedDate.Year > 2000
                select book;    
            
   }
   
   public IEnumerable<Book> BooksWithMoreThan250PagesWithWordsInAction() {
           //extension methods
        //return booksCollection.Where(p=> p.PageCount > 250 && p.Title.Contains("in Action"));
        // query expression
        return from l in booksCollection where l.PageCount > 250 && l.Title.Contains("in Action") select l;
   }

}