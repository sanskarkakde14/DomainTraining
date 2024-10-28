using System;
namespace Day5_Assignment
{
    public class LibraryService
    {
        private readonly IUserpermission _userPermissions;

        public LibraryService(IUserpermission userPermissions)
        {
            _userPermissions = userPermissions;
        }

        public void PerformBorrowBook(int bookId)
        {
            try
            {
                _userPermissions.BorrowBook(bookId);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("Operation not available: " + ex.Message);
            }
        }

        public void PerformReserveBook(int bookId)
        {
            try
            {
                _userPermissions.ReserveBook(bookId);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("Operation not available: " + ex.Message);
            }
        }

        public void PerformAddBook(int bookId, string bookTitle)
        {
            try
            {
                _userPermissions.AddBook(bookId, bookTitle);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("Operation not available: " + ex.Message);
            }
        }

        public void PerformRemoveBook(int bookId)
        {
            try
            {
                _userPermissions.RemoveBook(bookId);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("Operation not available: " + ex.Message);
            }
        }
    }
}

