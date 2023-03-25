using LibraryProject.Models;

namespace LibraryProject.Data.Infrastructure
{
    public interface IBook
    {
        List<Book> GetAllBooks();
        Book GetBook(int id);
        bool CheckStatus(int id);
        void CheckOutStatus(int id);
        void CheckInStatus(int id);
    }
}
