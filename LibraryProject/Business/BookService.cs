using LibraryProject.Data.Infrastructure;
using LibraryProject.Models;
using log4net;

namespace LibraryProject.Business
{
    public class BookService
    {
        private readonly IBook _bookContext;
        private readonly IBorrowing _borrowContent;
        private static readonly ILog log = LogManager.GetLogger(typeof(BookService));



        public BookService(IBook bookContext, IBorrowing barrowContent)
        {
            _bookContext = bookContext;
            _borrowContent = barrowContent;
            
        }

        public List<Book> GetBooks() {
            return _bookContext.GetAllBooks();
        }

        public Book GetBook(int id)
        {
            Book book = _bookContext.GetBook(id);
            if (book == null)
            {
                
                throw new InvalidOperationException();
                // Loglama yap
                
                
            }
            else {
                return book;
            }
            
        }

        public Detailsdto GetDetails(int id) {
            Book book = _bookContext.GetBook(id);
            if (book == null)
            {

                throw new InvalidOperationException();
                // Loglama yap


            }
            else
            {
                Detailsdto details = new Detailsdto();
                details.Book = book;
                Borrowing borrow = _borrowContent.GetLastBorrowingByBookId(id);
                if (borrow != null)
                {
                    details.CheckInDate = borrow.ActualReturnDate;
                    details.CheckOutDate = borrow.CheckedOutDate;
                    details.NameSurname = borrow.NameSurname;
                }
                else {
                    details.NameSurname = "Bu kitap daha önce ödünç alınmamış.";
                }
                
                
                return details;
                
                
            }
        }
    }
}
