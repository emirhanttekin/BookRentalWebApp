using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Data;
using WebApplication2.Data.Context;
using WebApplication2.Entity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class HireController : Controller
    {

        private readonly IHireBookRepository _hireBookRepository;
        private readonly IBookRepository _bookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public HireController(IHireBookRepository context, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _hireBookRepository = context;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
      
            List<Hire> objHiresList = _hireBookRepository.GetAll(includeProps:"Book").ToList();

            return View(objHiresList);
        }

        public IActionResult AddUpdate(int? id) //addBook ismi cshtml içersinde oluşturduğumuz AddBook cshtml çağırır aynı isim olduğu için isimlere dikkat etmeliyiz
        {

            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.BookName,
                    Value = k.Id.ToString() //verileri çektik 
                });
            ViewBag.BookList = BookList;  //controllerdan viewa veri aktarır
            if (id == null || id == 0)
            {
                //ekle
                return View();
            }
            else
            {
                //güncelle
                var hirepeOfVt = _hireBookRepository.Get(u => u.Id == id); //id bulur getirir nesneye çevirir  //T Get(Expression<Func<T, bool>> filtre)
                if (hirepeOfVt == null)
                {
                    return NotFound();
                }

                return View(hirepeOfVt);
            }
     
        }

        [HttpPost]
        public IActionResult AddUpdate(Hire hire )
        {


            //bunu frontend kısmındanda yapabilirsin 
            if (string.IsNullOrWhiteSpace(hire.StudentId.ToString()))
            {
                
                ModelState.AddModelError("Name", "Kitap Türü Adı alanı boş bırakılamaz.");
                return View(hire);
            }

            if (!ModelState.IsValid)
            {
                return View(hire);
            }

            try
            {
             

              
                if (hire.Id == 0)
                {
                    _hireBookRepository.AddBook(hire);
                    TempData["SUCCESSFUL"] = " Yeni Kiralama Kaydı Başarıyla Oluşturuldu!";
                }
                else
                {
                    _hireBookRepository.Update(hire);
                }

                _hireBookRepository.Save();
               
                return RedirectToAction("Index", "Hire");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu Lütfen Daha Sonra Tekrar Deneyin");
                return View(hire);
            }
        }



        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Hire hire = _hireBookRepository.Get(u => u.Id == id);
            if (hire == null)
            {
                return NotFound();
            }

            try
            {    
                _hireBookRepository.Delete(hire);
                _hireBookRepository.Save();
                TempData["SUCCESSFUL"] = " Kitap Türü Başarıyla Silindi!";
                return RedirectToAction("Index", "Hire");
                 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu, Lütfen Daha Sonra Tekrar Deneyin");
                return View("Index", _hireBookRepository.GetAll().ToList()); // Hata durumunda Index sayfasını tekrar yükle
            }
        }

    }
}

