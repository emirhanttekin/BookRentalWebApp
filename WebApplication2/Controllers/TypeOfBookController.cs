using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Context;
using WebApplication2.Entity;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class TypeOfBookController : Controller
    {
        private readonly ITypeOfBookRepository _typeOfBookRepository;
        

        public TypeOfBookController(ITypeOfBookRepository context)
        {
            _typeOfBookRepository = context;
        }

        public IActionResult Index()
        {
            List<TypeOfBook> objTypeOfBooks = _typeOfBookRepository.GetAll().ToList();

            return View(objTypeOfBooks);
        }

        public IActionResult
            AddBook() //addBook ismi cshtml içersinde oluşturduğumuz AddBook cshtml çağırır aynı isim olduğu için isimlere dikkat etmeliyiz
        {
            return View();

        }

        [HttpPost]
        public IActionResult AddBook(TypeOfBook typeOfBook)
        {


            //bunu frontend kısmındanda yapabilirsin 
            if (string.IsNullOrWhiteSpace(typeOfBook.Name))
            {
                ModelState.AddModelError("Name", "Kitap Türü Adı alanı boş bırakılamaz.");
                return View(typeOfBook);
            }

            if (!ModelState.IsValid)
            {
                return View(typeOfBook);
            }

            try
            {
                _typeOfBookRepository.AddBook(typeOfBook);
                _typeOfBookRepository.Save();
                TempData["SUCCESSFUL"] = "Yeni Kitap Türü Başarıyla Oluşturuldu!";
                return RedirectToAction("Index", "TypeOfBook");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu Lütfen Daha Sonra Tekrar Deneyin");
                return View(typeOfBook);
            }
        }


        public IActionResult Update(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            var typeOfVt = _typeOfBookRepository.Get( u =>u.Id == id); //id bulur getirir nesneye çevirir  //T Get(Expression<Func<T, bool>> filtre)
            if (typeOfVt == null)
            {
                return NotFound();
            }

            return View(typeOfVt);
        }
        [HttpPost]
        public IActionResult Update(TypeOfBook typeOfBook)
        {
            if (string.IsNullOrWhiteSpace(typeOfBook.Name))
            {
                ModelState.AddModelError("Name", "Kitap Türü Adı alanı boş bırakılamaz.");
                return View(typeOfBook);
            }

            if (!ModelState.IsValid)
            {
                return View(typeOfBook);
            }

            try
            {
                _typeOfBookRepository.Update(typeOfBook);
                _typeOfBookRepository.Save();
                TempData["SUCCESSFUL"] = " Kitap Türü Başarıyla Güncellendi!";
                return RedirectToAction("Index", "TypeOfBook");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu Lütfen Daha Sonra Tekrar Deneyin");
                return View(typeOfBook);
            }

            return View();



        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TypeOfBook typeOfBook = _typeOfBookRepository.Get(u => u.Id == id);
            if (typeOfBook == null)
            {
                return NotFound();
            }

            try
            {    
                _typeOfBookRepository.Delete(typeOfBook);
                 _typeOfBookRepository.Save();
                TempData["SUCCESSFUL"] = " Kitap Türü Başarıyla Silindi!";
                return RedirectToAction("Index", "TypeOfBook");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir Hata Oluştu, Lütfen Daha Sonra Tekrar Deneyin");
                return View("Index", _typeOfBookRepository.GetAll().ToList()); // Hata durumunda Index sayfasını tekrar yükle
            }
        }

    }
}

