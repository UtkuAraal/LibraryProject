using LibraryProject.Data.Infrastructure;
using LibraryProject.Models;
using LibraryProject.Utils;


namespace LibraryProject.Business
{
    public class BorrowingService
    {
        private readonly IBorrowing _borrowContext;
        private readonly IBook _bookContext;
        private readonly Validator _validator;
        public BorrowingService(IBorrowing borrowContext, IBook bookContext, Validator validator)
        {
            _borrowContext = borrowContext;
            _bookContext = bookContext;
            _validator = validator;
        }

        public void CheckOut(Borrowing borrowing) {
            var book = _bookContext.GetBook(borrowing.BookId);
            if (book == null)
            {
                Log.Info("Var olmayan bir kitap id'siyle CheckOut işlemi denendi!");
                throw new InvalidOperationException("Kitap bulunamadı!");

            }
            else if (!book.isAvailable)
            {
                Log.Info("Uygun durumda olmayan bir kitap id'siyle CheckOut işlemi denendi!");
                throw new InvalidOperationException("Bu kitap mevcut değil!");
            }
            else if (!_validator.TCKNValidator(borrowing.TCKN))
            {
                Log.Info("Yanlış TCKN numarası ile CheckOut işlemi denendi!");
                throw new InvalidOperationException("TCKN numarası doğru değil!");
            }
            else {
                _borrowContext.CheckOut(borrowing);
                _bookContext.CheckOutStatus(book.Id);
            }


        }

        public bool IsAvailable(int id) {
            var book = _bookContext.GetBook(id);
            if (book == null)
            {
                Log.Info("Var olmayan bir kitap id'siyle arama yapıldı!");
                throw new Exception("Bu id'ye sahip bir kitap bulunamadı!");

            }
            else if (!book.isAvailable)
            {
                return false;

            }
            else {
                return true;
            }
        }

        public Borrowing GetBorrowing(int id)
        {
            var book = _bookContext.GetBook(id);
            if (book == null)
            {
                Log.Info("Kaydı olmayan bir kitabın alım geçmişi arandı!");
                throw new Exception("Bu id'ye sahip bir kitap bulunamadı!");

            }
            else if (book.isAvailable)
            {
                Log.Info("Kütüphanede olan bir kitabın alım geçmişi arandı!");
                throw new Exception("Bu kitap zaten kütüphanede!");

            }
            else
            {
                return _borrowContext.GetBorrowingByBookId(id);
            }
        }

        public void CheckIn(int id) { 
            var borrow = _borrowContext.GetBorrowingById(id);
            if (borrow == null)
            {
                Log.Info("Var olmayan bir kitap id'siyle CheckIn işlemi denendi!");
                throw new Exception("Bu id'ye sahip bir kayıt bulunamadı!");
            }
            else if (borrow.ActualReturnDate != new DateTime(0001, 1, 1))
            {
                Log.Info("Zaten kütüphanede olan bir kitabın id'si ile CheckIn işlemi denendi!");
                throw new Exception("Bu id'ye sahip kayıt zaten kapatılmış!");
            }
            else {
                borrow.ActualReturnDate = DateTime.Today;
                _borrowContext.CheckIn(borrow);
                _bookContext.CheckInStatus(borrow.BookId);
            }
        
        }


    }
}
