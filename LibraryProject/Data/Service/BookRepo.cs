using LibraryProject.Data.Infrastructure;
using LibraryProject.Models;

namespace LibraryProject.Data.Service
{
    public class BookRepo : IBook 
    {
        private readonly ApplicationContext _appContext;

        public BookRepo(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public void CheckInStatus(int id)
        {
            var book = _appContext.Books.FirstOrDefault(book => book.Id == id);
            book.isAvailable = true;
            _appContext.SaveChanges();
        }

        public void CheckOutStatus(int id)
        {
            var book = _appContext.Books.FirstOrDefault(book => book.Id == id);
            book.isAvailable = false;
            _appContext.SaveChanges();
        }

        public bool CheckStatus(int id)
        {
            return _appContext.Books.FirstOrDefault(book => book.Id == id).isAvailable;
        }

        public List<Book> GetAllBooks()
        {
            return _appContext.Books.ToList();
        }

        public Book GetBook(int id)
        {
            return _appContext.Books.FirstOrDefault(b => b.Id == id);
        }
    }
}
