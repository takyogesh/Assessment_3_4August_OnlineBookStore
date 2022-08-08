using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookStore_WebApi.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string? Name { get; set; }
        [Required]
        public string? Zoner { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public string? Image { get; set; }


    }
}
