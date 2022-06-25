using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCFundamentals.Data;
using MVCFundamentals.Models;

namespace MVCFundamentals.Controllers
{
    public class CategoryController : Controller
    {

        private readonly MVCContext _db;
        //database'i referans al 
        public CategoryController(MVCContext db)
        {
            _db = db;
        }
        //string tipinde bir parametre alarak database'de içinde bu parametre
        //geçen verileri arayıp bu verileri ilgili viewa gönderir
        public IActionResult Index(string searchString)
        {
            var cars = from a in _db.Cars select a ;
           
            //parametre boş değilse aramayı yapar ve sonuçları cars a atar
            if(!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.ModelName!.Contains(searchString));  
            }

            return View(cars);
        }

        //GET - CREATE
        //Create New butonuna basildiginda ilgili sayfayi cagirir
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE 
        //Create sayfasinda bilgiler tamamlandiginda add butonuna basildiginda girilen bilgileri bir Car nesnesi
        // olarak tanimlayip post metodu ile database'e gonderir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car obj)
        {
            //sunucu tarafinda isvalid metoduyla girilen bilgilerin aranan şartlara uyup uymadığını kontrol eder
            if (ModelState.IsValid)
            {
                _db.Cars.Add(obj);
                _db.SaveChanges();
                //listeye geri dön
                return RedirectToAction("Index");  
            }
            return View(obj);
        }

        //GET-EDIT
        //Listeden ilgili ürünün düzenleme sayfasını getirir
        public   IActionResult Edit(int? id)
        {
            //gelen id veya database boş ise hata mesajı göster
            if (id == null || _db.Cars == null)
            {
                return NotFound();
            }
            //Bu iki satirdan birisi ile tıklanılan ürünün edit viewina ilgili ürünle beraber gider.
            var car = _db.Cars.FirstOrDefault(x => x.Id == id);
            // Eger asagidaki satiri kullanirsan fonksiyon türü Task<IActionResult> olmali???????
            //var car = await  _db.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        //POST-EDIT
        //Düzenleme bittikten sonra onaylama butonuna basıldığında veri değişikliklerini database e kaydeder
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Bind ile değiştirilebilmesini istediğin özellikleri gir
        public  IActionResult Edit(int id, [Bind("Id","ModelName","Color","ProductionDate","Price")] Car car)
        {
            //gelen id ve car objesinin id si uyuşuyor mu
            if(id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //uyuşuyorsa database e kaydetmeyi dene 
                try
                {
                    _db.Update(car);
                    _db.SaveChanges();
                }
                //hata olması durumunda CarExists fonksiyonuna id ile git
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(car);
        }


        //GET-DELETE
        //Tıklanılan ürünün delete sayfasını çağırır
        public IActionResult Delete(int? id)
        {

            if (id == null || _db.Cars == null)
            {
                return NotFound();
            }
            var car = _db.Cars.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        //POST-DELETE
        //Bir fonksiyonun aynı isim ve parametre ile 2 tane tanımı olamayacağı için
        //post methodunun ismini deleteconfirmed yapıp attiribute ile Delete yi tanımlıyorum
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult  DeleteConfirmed(int id)
        {

            if (_db.Cars == null)
            {
                return NotFound();
            }
            //ilgili id ile eşleşen veriyi bulup databaseden sil
            var car = _db.Cars.FirstOrDefault(x => x.Id == id);
            if (car != null)
            {
                _db.Cars.Remove(car);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        //Database de gönderilen id ile uyuşan bir veri olup olmadığını kontrol eder
        private bool CarExists(int id)
        {
            return (_db.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
