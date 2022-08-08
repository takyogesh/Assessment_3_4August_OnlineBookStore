using OnlineBookStore_WebApi.Data;
using OnlineBookStore_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OnlineBookStore_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private BooKListContext _context;
        public CartController(BooKListContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var crt = _context.Cart.ToList();
            var json= JsonConvert.SerializeObject(crt);
            return Ok(json);
        }
        [HttpPost]
        public IActionResult Create(cart data )
        {
            _context.Cart.Add(data);
            _context.SaveChanges(); 
            return Ok();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cart = _context.Cart.Where(x => x.Id == id).FirstOrDefault();
            if (cart != null)
            {
                _context.Cart.Remove(cart);
                _context.SaveChanges();
                return Ok();
            }
            else
                return NoContent();
        }
    }
}
