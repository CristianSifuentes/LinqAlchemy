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

    public IEnumerable<Book> SearchBooks(string term)
    {
        return booksCollection.AsQueryable()
            .TextFilter(term)
            .ToList();
    }

    public IEnumerable<Book> BooksAfter2000()
    {
        return from book in booksCollection
               where book.PublishedDate.Year > 2000
               select book;
    }
	
	public bool AllBooksHaveStatus(){  
	  return booksCollection.All(p=> p.Status!= string.Empty);
	}

	public bool IfAnyBookWasPublished2005 (
	){
        return booksCollection.Any(p => p.PublishedDate.Year == 2005);
    }


    public IEnumerable<Book> BooksWithMoreThan250PagesWithWordsInAction()
    {
        return from book in booksCollection
               where book.PageCount > 250 && book.Title.Contains("in Action")
               select book;
    }
	
	public IEnumerable<Book> PythonBooks(){
	
     return booksCollection.Where(p => p.Categories.Contains("Python"));
    }
	
	public IEnumerable<Book> BooksOver450PagesSortedByPageNumberInDescendingOrder(){
	   return booksCollection.Where(p => p.PageCount > 450).OrderByDescending(p=> p.PageCount);
	}
	
	public IEnumerable<Book> FirstThreeJavaBooksOrderedByDate(){
	
     return booksCollection
        .Where(p=> p.Categories.Contains("Java"))
        .OrderBy(p=> p.PublishedDate)
        .TakeLast(3);
	}
	
	public IEnumerable<Book> ThirdAndFourthBookofMoreThan400Pages(){  
        return booksCollection
        .Where(p=> p.PageCount > 400)
        .Take(4)
        .Skip(2);
	}
	
	public IEnumerable<Book> FirstThreeBooksofTheCollection(){
	

      return booksCollection.Take(3)
        .Select(p=> new Book() { Title= p.Title, PageCount= p.PageCount  });
		
		}


}