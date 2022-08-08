namespace OnlineBookStore_WebApp.Models
{
    public class BookWebApp
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Zoner { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Cost { get; set; }
        public IFormFile? Image { get; set; }


    }
}
