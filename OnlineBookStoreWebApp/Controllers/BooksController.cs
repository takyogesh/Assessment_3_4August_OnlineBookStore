using OnlineBookStore_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OnlineBookStore_WebApp.Controllers
{
    public class BooksController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7021/");
        HttpClient httpClient = new HttpClient();
        private IWebHostEnvironment _env;
        private Book _book;
        private string folder = "images/cover";
        

        public BooksController(Book book,IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
            _book = book;
            httpClient.BaseAddress = baseAddress;
        }
        public ActionResult Index()
        {
            HttpResponseMessage Response = httpClient.GetAsync(baseAddress + "Books/Index").Result;
            if (Response.IsSuccessStatusCode)
            {
                string data = Response.Content.ReadAsStringAsync().Result;
                List<Book> list = new List<Book>();
                list = JsonConvert.DeserializeObject<List<Book>>(data);
                return View(list);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(string searchQuery)
        {
            HttpResponseMessage response = httpClient.GetAsync(baseAddress + "Books/Search?Query=" + searchQuery).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(data);
                return View(books);
            }
            return View();
        }
        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync(baseAddress + "Books/Details?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Book book = JsonConvert.DeserializeObject<Book>(data);  
                return View(book);
            }
            return View();
        }
        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookWebApp book)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
               
                if (book != null)
                {
                    folder += Guid.NewGuid().ToString() + book.Image.FileName;
                    string imageForWebApiSide = folder;
                    string serverFolder = Path.Combine(_env.WebRootPath, folder);
                    await book.Image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    var bok = JsonConvert.SerializeObject(book);
                    _book.Name=book.Name;
                    _book.Zoner = book.Zoner;
                    _book.Cost=book.Cost;
                    _book.Image = imageForWebApiSide;
                    _book.ReleaseDate=book.ReleaseDate;
                    var stringdata=JsonConvert.SerializeObject(_book);
                    var contentData = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = httpClient.PostAsync(baseAddress + "Books/Create", contentData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                return View();
            }
            return Ok();
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = httpClient.GetAsync(baseAddress + "Books/Details?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Book book = JsonConvert.DeserializeObject<Book>(data);
                return View(book);
            }
            return View();
        }
        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            try
            {
                var stringdata= JsonConvert.SerializeObject(book);
                var contentData = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = httpClient.PutAsync(baseAddress + "Books/Edit", contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return Ok();
            }
            catch
            {
                return View();
            }
        }
        // GET: BooksController/Delete/5
        [HttpGet("id")]
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = httpClient.DeleteAsync(baseAddress + "Books/Delete?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Ok();
            }
        }
        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(baseAddress + "Books/Details?id=" + id).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;

                var book = JsonConvert.DeserializeObject<Book>(stringData);
                var cart = new cart()
                {
                    Name = book.Name,
                    Cost = book.Cost,
                    Zoner = book.Zoner,
                    Image = book.Image,

                };
                var jsondata= JsonConvert.SerializeObject(cart);
                var contentData = new StringContent(jsondata, System.Text.Encoding.UTF8, "application/json");

                response = httpClient.PostAsync(baseAddress + "Cart/Create", contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Viewcart));
                }
                else
                    return View();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Viewcart()
        {
            HttpResponseMessage Response = httpClient.GetAsync(baseAddress + "Cart/Index").Result;
            if (Response.IsSuccessStatusCode)
            {
                string data = Response.Content.ReadAsStringAsync().Result;
               List<cart> cartList = JsonConvert.DeserializeObject<List<cart>>(data);
                return View(cartList);
            }
            else
                return View();
        }
    }
}
