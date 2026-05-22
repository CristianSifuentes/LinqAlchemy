# LinqAlchemy

LinqAlchemy is a polished .NET console application that demonstrates how to load rich JSON book data, convert it into a strongly typed model, and execute powerful LINQ queries. The code is built for readability, extension, and real-world dynamic filtering.

## Project Overview

This repository is focused on:

- Loading a JSON book catalog into a typed `Book` model
- Using LINQ and expression-based filters to query data
- Exposing both static and dynamic query patterns
- Demonstrating how to safely consume external JSON data in .NET

## What You Will Learn

- How to model JSON data in C# with `Book` objects
- How to safely deserialize JSON with `System.Text.Json`
- How to write `IQueryable`-based dynamic text filtering
- How to combine LINQ query expressions with reusable search logic
- How to add boolean queries for data validation and summary checks

## Repository Structure

- `Program.cs` - console application entry point and sample execution scenarios
- `Book.cs` - domain model for book metadata
- `LinqQueries.cs` - query service and repository for books
- `DynamicQueryExtensions.cs` - reusable dynamic search extension methods
- `books.json` - sample dataset with extensive book metadata
- `curso-linq.csproj` - .NET 6 console app project file
- `README.md` - project documentation

## Key Code Components

### 1. `Program.cs`

This file demonstrates how the application executes queries and prints results to the console.

Current operations in the sample run:

- Print the full book collection
- Print books with more than 250 pages and "in Action" in the title
- Print books published after 2000
- Execute dynamic text searches for `android`, `java`, and `c#`
- Validate if all books have a status
- Check whether any book was published in 2005

This sample flow is designed to show both static LINQ queries and dynamic runtime filtering.

### 2. `Book.cs`

The `Book` model captures the data fields used by the application. It is intentionally aligned with the JSON schema while keeping the domain model simple.

Important properties:

- `Title`
- `PageCount`
- `Status`
- `PublishedDate`
- `Authors`
- `Categories`

The model is ideal for LINQ queries, filtering, grouping, and extension to additional query features.

### 3. `LinqQueries.cs`

This class is the main data service for the application.

Features included:

- Safe loading of `books.json` from `AppContext.BaseDirectory` and fallback to the current working directory
- Deserialization with a safe default list when JSON is missing or invalid
- Static query methods:
  - `GetAllBooks()`
  - `BooksAfter2000()`
  - `BooksWithMoreThan250PagesWithWordsInAction()`
- Boolean summary methods:
  - `AllBooksHaveStatus()`
  - `IfAnyBookWasPublished2005()`
- Dynamic text search method:
  - `SearchBooks(string term)`

Example methods:

```csharp
public IEnumerable<Book> SearchBooks(string term)
{
    return booksCollection.AsQueryable()
        .TextFilter(term)
        .ToList();
}

public bool AllBooksHaveStatus()
{
    return booksCollection.All(p => p.Status != string.Empty);
}

public bool IfAnyBookWasPublished2005()
{
    return booksCollection.Any(p => p.PublishedDate.Year == 2005);
}
```

### 4. `DynamicQueryExtensions.cs`

This file contains reusable extension methods that implement dynamic text filtering using expression trees.

The core feature is `TextFilter`, which works in two modes:

- `IQueryable<T>.TextFilter(string term)` — strongly typed generic filter
- `IQueryable.TextFilter(string term)` — untyped filter for runtime query scenarios

How it works:

- Identifies all `string` properties on the target type
- Builds an expression tree that checks each property for the search term
- Combines property checks using `||`
- Applies case-insensitive filtering via `ToLowerInvariant()`

This is the same technique used in production-quality dynamic search systems, and it avoids hard-coded switch cases.

### 5. `books.json`

The JSON file includes a sample book catalog with rich metadata.

Example fields:

- `title`
- `pageCount`
- `publishedDate`
- `thumbnailUrl`
- `shortDescription`
- `longDescription`
- `status`
- `authors`
- `categories`

The dataset contains more information than the current model consumes, which makes the project easy to extend for new query and display features.

### 6. `curso-linq.csproj`

The project file configures the .NET console app.

Key settings:

- `OutputType`: `Exe`
- `TargetFramework`: `net6.0`
- `RootNamespace`: `LinqAlchemy`
- `ImplicitUsings`: `enable`
- `Nullable`: `enable`

It also ensures `books.json` is copied to the output folder on build.

## Build and Run Instructions

Build the project:

```bash
dotnet build
```

Run the project:

```bash
dotnet run
```

Expected behavior:

- `books.json` is loaded successfully
- The application prints the full book list
- Static LINQ query results are shown
- Dynamic text search results for terms like `android`, `java`, and `c#` are displayed
- Boolean validation output is shown for status and publication year

## Console Output Example

The sample output includes several query results. Example sections are:

- Full book collection table
- Books matching `android`
- Books matching `java`
- Books matching `c#`
- Boolean summary lines:
  - `All books have status: true`
  - `Any book published in 2005: false`

## Future Enhancements

This project is designed for iterative improvement.

Recommended next steps:

- Add `GetBooksByCategory(string category)`
- Add `GetBooksByAuthor(string author)`
- Add pagination and sorting options
- Add unit tests for `DynamicQueryExtensions` and `LinqQueries`
- Add command-line arguments for dynamic queries
- Add additional output formats such as CSV, JSON, or HTML

## Why This Project Matters

`LinqAlchemy` is more than a sample app: it is a practical template for building dynamic querying systems in .NET.

It shows how to:

- safely consume external JSON data
- build reusable LINQ query services
- add runtime text search without hard-coded switch logic
- keep query behavior explicit, testable, and maintainable

---

Thank you for exploring `LinqAlchemy`. This project is a strong foundation for building powerful, dynamic data applications with C# and LINQ.
