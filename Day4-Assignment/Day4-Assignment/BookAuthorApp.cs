using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
namespace Day4_Assignment
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
    }
    public class BookAuthorApp
	{
		public void BookAuthor()
		{
            var authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "J.K. Rowling", Country = "UK" },
                new Author { AuthorId = 2, Name = "George R.R. Martin", Country = "USA" },
                new Author { AuthorId = 3, Name = "J.R.R. Tolkien", Country = "UK" },
                new Author { AuthorId = 4, Name = "Agatha Christie", Country = "UK" },
                new Author { AuthorId = 5, Name = "Mark Twain", Country = "USA" }
            };

            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "Harry Potter and the Sorcerer's Stone", YearPublished = 1997, AuthorId = 1 },
                new Book { BookId = 2, Title = "A Game of Thrones", YearPublished = 1996, AuthorId = 2 },
                new Book { BookId = 3, Title = "The Hobbit", YearPublished = 1937, AuthorId = 3 },
                new Book { BookId = 4, Title = "Murder on the Orient Express", YearPublished = 1934, AuthorId = 4 },
                new Book { BookId = 5, Title = "Adventures of Huckleberry Finn", YearPublished = 1884, AuthorId = 5 }
            };

            // Convert to JSON and save
            string jsonAuthors = JsonSerializer.Serialize(authors);
            File.WriteAllText("authors.json", jsonAuthors);

            string jsonBooks = JsonSerializer.Serialize(books);
            File.WriteAllText("books.json", jsonBooks);

            // Convert to XML and save
            var xmlSerializerAuthors = new XmlSerializer(typeof(List<Author>));
            using (var writer = new StreamWriter("authors.xml"))
            {
                xmlSerializerAuthors.Serialize(writer, authors);
            }

            var xmlSerializerBooks = new XmlSerializer(typeof(List<Book>));
            using (var writer = new StreamWriter("books.xml"))
            {
                xmlSerializerBooks.Serialize(writer, books);
            }

            // Read and display JSON data
            Console.WriteLine("Authors from JSON:");
            var jsonAuthorsData = File.ReadAllText("authors.json");
            var deserializedAuthors = JsonSerializer.Deserialize<List<Author>>(jsonAuthorsData);
            foreach (var author in deserializedAuthors)
            {
                Console.WriteLine($"ID: {author.AuthorId}, Name: {author.Name}, Country: {author.Country}");
            }

            Console.WriteLine("\nBooks from JSON:");
            var jsonBooksData = File.ReadAllText("books.json");
            var deserializedBooks = JsonSerializer.Deserialize<List<Book>>(jsonBooksData);
            foreach (var book in deserializedBooks)
            {
                Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Year: {book.YearPublished}, Author ID: {book.AuthorId}");
            }

            // Read and display XML data
            Console.WriteLine("\nAuthors from XML:");
            using (var reader = new StreamReader("authors.xml"))
            {
                var deserializedAuthorsXml = (List<Author>)xmlSerializerAuthors.Deserialize(reader);
                foreach (var author in deserializedAuthorsXml)
                {
                    Console.WriteLine($"ID: {author.AuthorId}, Name: {author.Name}, Country: {author.Country}");
                }
            }

            Console.WriteLine("\nBooks from XML:");
            using (var reader = new StreamReader("books.xml"))
            {
                var deserializedBooksXml = (List<Book>)xmlSerializerBooks.Deserialize(reader);
                foreach (var book in deserializedBooksXml)
                {
                    Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Year: {book.YearPublished}, Author ID: {book.AuthorId}");
                }
            }

            Console.ReadLine();
        }
    }
	}


