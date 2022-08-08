using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookStore_WebApi.Models
{
    public class cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Zoner { get; set; }   
        [Required]
        public int Cost { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
