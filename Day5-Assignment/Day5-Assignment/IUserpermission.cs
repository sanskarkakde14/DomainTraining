using System;
namespace Day5_Assignment
{
    public interface IUserpermission
    {
        void BorrowBook(int bookId);
        void ReserveBook(int bookId);
        void AddBook(int bookId, string bookTitle);
        void RemoveBook(int bookId);
    }
}

