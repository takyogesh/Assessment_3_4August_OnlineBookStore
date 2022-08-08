namespace OnlineBookStore_WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Zoner { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Cost { get; set; }
        public string? Image { get; set; }
    }
}
