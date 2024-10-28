using System;
namespace Day5_Assignment
{
    public class Student : IUserpermission
    {
        public void BorrowBook(int bookId)
        {
            Console.WriteLine("Student borrowed a book.");
        }

        public void ReserveBook(int bookId)
        {
            throw new NotImplementedException("Students cannot reserve books.");
        }

        public void AddBook(int bookId, string bookTitle)
        {
            throw new NotImplementedException("Students cannot add books.");
        }

        public void RemoveBook(int bookId)
        {
            throw new NotImplementedException("Students cannot remove books.");
        }
    }

    public class Teacher : IUserpermission
    {
        public void BorrowBook(int bookId)
        {
            Console.WriteLine("Teacher borrowed a book.");
        }

        public void ReserveBook(int bookId)
        {
            Console.WriteLine("Teacher reserved a book.");
        }

        public void AddBook(int bookId, string bookTitle)
        {
            throw new NotImplementedException("Teachers cannot add books.");
        }

        public void RemoveBook(int bookId)
        {
            throw new NotImplementedException("Teachers cannot remove books.");
        }
    }

    public class Librarian : IUserpermission
    {
        public void BorrowBook(int bookId)
        {
            Console.WriteLine("Librarian borrowed a book.");
        }

        public void ReserveBook(int bookId)
        {
            Console.WriteLine("Librarian reserved a book.");
        }

        public void AddBook(int bookId, string bookTitle)
        {
            Console.WriteLine("Librarian added a book.");
        }

        public void RemoveBook(int bookId)
        {
            Console.WriteLine("Librarian removed a book.");
        }
    }

}

