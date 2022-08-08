using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookStore_WebApi.Data;
using OnlineBookStore_WebApi.Models;

namespace OnlineBookStore_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BooksController : Controller
    {
        private BooKListContext _context;

        // GET: BooksController
        public BooksController(BooKListContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var books =  _context.Books.ToList();
            var bookjson = JsonConvert.SerializeObject(books);
            return Ok(bookjson); 
        }
        // GET: BooksController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            var books = _context.Books.Where(b => b.Id == id).FirstOrDefault();
            var bookjson = JsonConvert.SerializeObject(books);  
            return Ok(bookjson);
        }
        // POST: BooksController/Create
        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return Ok();
                
            }
            catch
            {
                return BadRequest();
            }
           
        }
        // POST: BooksController/Edit/5
        [HttpPut]
        public ActionResult Edit(Book book)
        {
            try
            {
                var editbook = _context.Books.Where(b=>b.Id== book.Id).FirstOrDefault();
                if (editbook != null)
                {
                    editbook.Name = book.Name;
                    editbook.Zoner= book.Zoner; 
                    editbook.Cost= book.Cost;   
                    editbook.ReleaseDate = book.ReleaseDate;    
                    _context.Books.Update(editbook);
                    _context.SaveChanges();
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        // GET: BooksController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var delete=_context.Books.Where(b=>b.Id== id).FirstOrDefault(); 
            if(delete != null)
            {
                _context.Books.Remove(delete);  
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Search(string Query)
        {
            var querystring = _context.Books.Where(b => b.Name== Query || b.Zoner== Query).ToList();
            var json=JsonConvert.SerializeObject(querystring);
            return Ok(json);
        }
    }
}
