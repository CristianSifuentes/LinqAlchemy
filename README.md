# LinqAlchemy

LinqAlchemy is a small, professional .NET console application that demonstrates how to load structured JSON data and present it using a clean object model. The project is built with C# and .NET 6, and it is designed to be easy to understand, extend, and document.

## Project Purpose

This repository serves as a learning foundation for LINQ-based data processing and simple console-driven data presentation. It loads a JSON book catalog, converts the JSON objects into strongly typed `Book` instances, and prints a formatted table of book details.

## What You Will Learn

- How to model JSON data with C# classes
- How to deserialize JSON into a `List<T>` with `System.Text.Json`
- How to structure a clean console application with top-level statements
- How to separate concerns between data loading and output formatting
- How to prepare a project for future LINQ query expansion

## Repository Structure

- `Program.cs` - application entry point and presentation logic
- `Book.cs` - domain model representing book metadata
- `LinqQueries.cs` - data access class for loading the book catalog
- `books.json` - sample data source with rich book metadata in JSON format
- `curso-linq.csproj` - .NET 6 console app project file
- `README.md` - project documentation

## Step-by-Step Code Walkthrough

### 1. `Program.cs`

This file contains the entry point for the console application.

Key responsibilities:

- Create an instance of `LinqQueries`
- Request the complete book collection with `GetAllBooks()`
- Print a neatly formatted table using the `PrintValues` method

Important code flow:

```csharp
LinqQueries queries = new LinqQueries();
PrintValues(queries.GetAllBooks());
```

The `PrintValues` method is responsible for rendering the table header and each book row in a readable console format.

### 2. `Book.cs`

This class defines the book model used throughout the application. It intentionally includes fields that match the JSON payload, while keeping the domain model clean and straightforward.

Properties:

- `Title` - the name of the book
- `PageCount` - the number of pages in the book
- `Status` - publication status, e.g. `PUBLISH`
- `PublishedDate` - publication date as a `DateTime`
- `Authors` - array of author names
- `Categories` - array of categories or genres

This model is suitable for LINQ operations, filtering, grouping, and sorting in a future enhancement.

### 3. `LinqQueries.cs`

This class encapsulates data loading and serves as the foundation for future LINQ query methods.

Current responsibilities:

- Read `books.json` from disk using `StreamReader`
- Deserialize JSON into a `List<Book>` using `System.Text.Json`
- Provide the `GetAllBooks()` method for consumers

Current implementation:

```csharp
public IEnumerable<Book> GetAllBooks()
{
    return booksCollection;
}
```

This is the perfect place to add new LINQ query methods, such as:

- `GetPublishedBooks()`
- `GetBooksByCategory(string category)`
- `GetBooksByAuthor(string author)`
- `GetBooksPublishedAfter(DateTime date)`

### 4. `books.json`

The JSON file contains a sample dataset representing a wide collection of books. Each object includes metadata such as:

- `title`
- `pageCount`
- `publishedDate`
- `thumbnailUrl`
- `shortDescription`
- `longDescription`
- `status`
- `authors`
- `categories`

The data file contains more fields than the current model consumes. This makes the data source ideal for future feature expansion while keeping the initial implementation focused and easy to understand.

### 5. `curso-linq.csproj`

The project file defines the application type and target framework.

Key configuration:

- `OutputType`: `Exe`
- `TargetFramework`: `net6.0`
- `RootNamespace`: `LinqAlchemy`
- `ImplicitUsings`: `enable`
- `Nullable`: `enable`

This modern configuration reduces boilerplate and enables nullable reference type safety.

## Build and Run Instructions

To build the project, run:

```bash
dotnet build
```

To execute the application, run:

```bash
dotnet run
```

Expected behavior:

- The application loads `books.json`
- It deserializes the JSON into a typed `List<Book>`
- It prints a table with `Title`, `Pages`, and `Published Date`

## Example Console Output

The output is formatted with aligned columns, for example:

```
Title                                                         Pages      Published Date
Unlocking Android                                              416      04/01/2009
Android in Action, Second Edition                              592      01/14/2011
Specification by Example                                         0      06/03/2011
```

## Future Enhancements

This codebase is intentionally compact and ready for extension.

Recommended next steps:

- Add LINQ query methods in `LinqQueries.cs`
- Implement filtering and sorting options
- Add unit tests for data loading and formatting
- Support additional output formats (CSV, JSON, HTML)
- Add command-line options for dynamic queries
- Add `BookRepository` or service layer for better separation of concerns

## Why This Documentation Matters

This README is designed to provide an immediate, high-quality introduction to the codebase, while also serving as a foundation for polished future development. It explains the current implementation clearly and describes where to extend the application next.

## Contribution Notes

If you want to expand this project, start by adding query methods to `LinqQueries.cs` and updating the presentation logic in `Program.cs`. Keep the project structure clean, and document each new feature in this README so the project remains professional and maintainable.

---

Thank you for exploring `LinqAlchemy`. This project is a strong starting point for building amazing LINQ-powered applications in .NET.
