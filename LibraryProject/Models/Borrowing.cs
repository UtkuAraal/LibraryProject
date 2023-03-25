using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        
        public DateTime CheckedOutDate { get; set; } = DateTime.Today;

        public DateTime ExpectedReturnDate { get; set; } = DateTime.Today.AddDays(15);

        [Required(ErrorMessage ="İsim soyisim bilgisi gereklidir!")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage ="Telefon numarası gereklidir!")]
        [RegularExpression(@"^(05)([0-9]{2})\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})$", ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="TCKN gereklidir!")]
        [RegularExpression(@"^[1-9]{1}[0-9]{9}[02468]{1}$", ErrorMessage = "Lütfen geçerli bir TC Kimlik Numarası giriniz.")]
        public string TCKN { get; set; }

        public DateTime ActualReturnDate { get; set; }
        
    }
}
