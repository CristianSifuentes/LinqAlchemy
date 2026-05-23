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
        .Select(p=> new Book() { Title= p.Title, PageCount= p.PageCount, Authors= p.Authors, Categories= p.Categories, PublishedDate= p.PublishedDate, Status= p.Status });	
	}


        public long CountBooksBetween200And500Pages()
    {
        return booksCollection.LongCount(p=> p.PageCount>=200 && p.PageCount<=500);
    }

    public DateTime EarliestPublicationDate()
    {
        return booksCollection.Min(p=> p.PublishedDate);
    }

    public int MaxPageCount()
    {
        return booksCollection.Max(p=> p.PageCount);
    }

    public Book BookWithFewestPages()
    {
        return booksCollection.Where(p=> p.PageCount>0).MinBy(p=> p.PageCount);
    }

    public Book MostRecentlyPublishedBook()
    {
        return booksCollection.MaxBy(p => p.PublishedDate);
    }

    public int SumOfPagesForBooksBetween0And500()
    {
        return booksCollection.Where(p=> p.PageCount >= 0 && p.PageCount <=500).Sum(p=> p.PageCount);
    }
    
    public string ConcatenatedTitlesAfter2015()
    {
        return booksCollection
                .Where(p=> p.PublishedDate.Year > 2015)
                .Aggregate("", (TitulosLibros, next) =>
                {
                    if(TitulosLibros != string.Empty)
                        TitulosLibros += " - " + next.Title;
                    else
                        TitulosLibros += next.Title;

                    return TitulosLibros;
                });
    }

    public double AverageTitleCharacters()
    {
        return booksCollection.Average(p=> p.Title.Length);
    }

    public IEnumerable<IGrouping<int, Book>> BooksAfter2000GroupedByYear()
    {
        return booksCollection.Where(p=> p.PublishedDate.Year >= 2000).GroupBy(p=> p.PublishedDate.Year);
    }

    public ILookup<char, Book> BooksLookupByFirstLetter()
    {
        return booksCollection.ToLookup(p=> p.Title[0], p=> p);
    }

    public IEnumerable<Book> BooksAfter2005WithMoreThan500Pages()
    {
        var LibrosDepuesdel2005 = booksCollection.Where(p=> p.PublishedDate.Year > 2005);

        var LibrosConMasde500pag = booksCollection.Where(p=> p.PageCount > 500);

        return LibrosDepuesdel2005.Join(LibrosConMasde500pag, p=> p.Title, x=> x.Title, (p, x) => p);
    }


}