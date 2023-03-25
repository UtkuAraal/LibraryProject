using LibraryProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Reflection.Metadata;

namespace LibraryProject.Data.Service
{
    public class BorrowingRepo : IBorrowing
    {
        private readonly ApplicationContext _context;
        public BorrowingRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CheckIn(Borrowing borrowing)
        {
            try
            {
                _context.Borrowings.Update(borrowing);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Borrowing GetBorrowingByBookId(int id) {
            var latestBorrowing = _context.Borrowings
                .Where(b => b.BookId == id && b.ActualReturnDate == new DateTime(0001, 1, 1))
                .OrderByDescending(b => b.CheckedOutDate)
                .FirstOrDefault();
            return latestBorrowing;
        }

        public void CheckOut(Borrowing borrowing)
        {
            try {
                _context.Add(borrowing);
                _context.SaveChanges();
            }
            catch (Exception exception) {
                throw exception;
            }
            
        }

        public Borrowing GetBorrowingById(int id)
        {
            return _context.Borrowings.FirstOrDefault(b => b.Id == id);

        }

        public Borrowing GetLastBorrowingByBookId(int id)
        {
            var latestBorrowing = _context.Borrowings
                .Where(b => b.BookId == id)
                .OrderByDescending(b => b.CheckedOutDate)
                .FirstOrDefault();
            return latestBorrowing;
        }

    }
}
