using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool isAvailable { get; set; } = true;
    }
}
