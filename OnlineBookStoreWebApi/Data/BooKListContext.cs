using OnlineBookStore_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookStore_WebApi.Data
{
    public class BooKListContext:DbContext
    {
        public BooKListContext(DbContextOptions<BooKListContext> options) :
           base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<cart> Cart { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>().HasIndex(b => b.Name).IsUnique();


        }


    }
}
