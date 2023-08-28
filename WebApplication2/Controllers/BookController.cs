using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using WebApplication2.Data.Context;
using WebApplication2.Entity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class BookController : Controller
    {

        private readonly IBookRepository  _bookRepository;
        private readonly ITypeOfBookRepository _typeOfBookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;


        public BookController(IBookRepository context, ITypeOfBookRepository typeOfBookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = context;
            _typeOfBookRepository = typeOfBookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
      
            List<Book> objBooks = _bookRepository.GetAll(includeProps:"TypeOfBook").ToList();

            return View(objBooks);
        }

        public IActionResult AddUpdate(int? id) //addBook ismi cshtml içersinde oluşturduğumuz AddBook cshtml çağırır aynı isim olduğu için isimlere dikkat etmeliyiz
        {

            IEnumerable<SelectListItem> TypeOfBookList = _typeOfBookRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString() //verileri çektik 
                });
            ViewBag.TypeOfBookList = TypeOfBookList;  //controllerdan viewa veri aktarır
            if (id == null || id == 0)
            {
                //ekle
                return View();
            }
            else
            {
                //güncelle
                var typeOfVt = _bookRepository.Get(u => u.Id == id); //id bulur getirir nesneye çevirir  //T Get(Expression<Func<T, bool>> filtre)
                if (typeOfVt == null)
                {
                    return NotFound();
                }

                return View(typeOfVt);
            }
     
        }

        [HttpPost]
        public IActionResult AddUpdate(Book book, IFormFile? file )
        {


            //bunu frontend kısmındanda yapabilirsin 
            if (string.IsNullOrWhiteSpace(book.BookName))
            {
                
                ModelState.AddModelError("Name", "Kitap Türü Adı alanı boş bırakılamaz.");
                return View(book);
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            try
            {
                //var errors = ModelState.Values.SelectMany(x => x.Errors); //HATA MESAJI İÇİN debug yaptığında görüyorsun
                string wwwRootPath = _webHostEnvironment.WebRootPath; //www.rootunu veriyor 
                string bookPath = Path.Combine(wwwRootPath, @"img");
                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream); //kopyalıyor
                    }
                    book.ImageUrl = @"\img\" + file.FileName;
                }
             

              
                if (book.Id == 0)
                {
                    _bookRepository.AddBook(book);
                    TempData["SUCCESSFUL"] = " Kitap Güncelleme Başarıyla Oluşturuldu!";
                }
                else
                {
                    _bookRepository.Update(book);
                }
              
                _bookRepository.Save();
               
                return RedirectToAction("Index", "Book");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu Lütfen Daha Sonra Tekrar Deneyin");
                return View(book);
            }
        }



        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Book book = _bookRepository.Get(u => u.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            try
            {    
                _bookRepository.Delete(book);
                 _bookRepository.Save();
                TempData["SUCCESSFUL"] = " Kitap Türü Başarıyla Silindi!";
                return RedirectToAction("Index", "Book");
                 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu, Lütfen Daha Sonra Tekrar Deneyin");
                return View("Index", _bookRepository.GetAll().ToList()); // Hata durumunda Index sayfasını tekrar yükle
            }
        }

    }
}

