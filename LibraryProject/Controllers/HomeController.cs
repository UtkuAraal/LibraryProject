using LibraryProject.Business;
using LibraryProject.Models;
using LibraryProject.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookService _bookService;
        private readonly BorrowingService _borrowingService;
        

        public HomeController(ILogger<HomeController> logger, BookService bookService, BorrowingService borrowingService)
        {
            _logger = logger;
            _bookService = bookService;
            _borrowingService = borrowingService;
            
        }

        public IActionResult Index()
        {
            
            var books = _bookService.GetBooks();
            return View(books);
        }

       public IActionResult Details(int id)
        {
            try {
                return View(_bookService.GetDetails(id));
            }
            catch(InvalidOperationException ex) {
                Log.Error("Details sayfasında bir hata meydana geldi!");
                return RedirectToAction("Index", "Home");
            } 
        }

        
        public IActionResult CheckOutForm(int id) {
            try {
                if (_borrowingService.IsAvailable(id))
                {
                    ViewBag.Id = id;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }catch(Exception ex)
            {
                Log.Error("CheckOutForm sayfasında bir hata meydana geldi!");
                return RedirectToAction("Index", "Home");
            }
            
            
        }

        
        public IActionResult CheckOut(int id, [FromForm] Borrowing borrowing)
        {
           
            try {
                
                borrowing.BookId = id;
                _borrowingService.CheckOut(borrowing);
                
                
            }
            catch (Exception ex) {
                Log.Error("CheckOut sayfasında bir hata meydana geldi!");

            }
            return RedirectToAction("Index", "Home");


        }

        public IActionResult CheckInForm(int id)
        {
            try
            {
                if (!_borrowingService.IsAvailable(id))
                {
                    ViewBag.Id = id;
                    var borrow = _borrowingService.GetBorrowing(id);

                    if (DateTime.Today > borrow.ExpectedReturnDate)
                    {
                        TimeSpan delay = DateTime.Today - borrow.ExpectedReturnDate;
                        int delayDays = delay.Days;
                        int penalty = delayDays * 5; // ceza miktarı hesaplanıyor
                        ViewBag.Penalty = penalty; // View'da göstermek için ViewBag'e ceza miktarı atanıyor
                    }
                    else
                    {
                        ViewBag.Penalty = 0; // Beklenen tarihten erken teslim edildiyse ceza miktarı sıfır olarak atanıyor
                    }


                    return View(borrow);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                Log.Error("CheckInForm sayfasında bir hata meydana geldi!");
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult CheckIn(int id) {
            try {
                _borrowingService.CheckIn(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) {
                Log.Error("CheckIn sayfasında bir hata meydana geldi!");
                return RedirectToAction("Index", "Home");
            }
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}