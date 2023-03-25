namespace LibraryProject.Models
{
    public interface IBorrowing
    {
        void CheckOut(Borrowing borrowing);
        void CheckIn(Borrowing borrowing);
        Borrowing GetBorrowingByBookId(int id);
        Borrowing GetBorrowingById(int id);
        Borrowing GetLastBorrowingByBookId(int id);


    }
}
