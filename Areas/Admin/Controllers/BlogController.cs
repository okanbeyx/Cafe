using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Cafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;//blog verlerinin veritabanından çekilmesini sağlar
        private readonly IWebHostEnvironment _he; //sunucu tarafından dosya yolları ve ortam bilgisine erişim sağlanır
												  //IWebHostEnvironment uygulamanın çalıştığı ortam hakkında bilgi almaya olanak sağlayan
                                                  //bir arabirimdir

		public BlogController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Blog
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());//tüm blog kayıtları listelenir 
                                                            //asyncblog tablosundaki tüm kayıtlları "asenkron" bir biçimde çekmek için
                                                            //await ise bu asenkonun bitmsii bekler sonucu o na göre atar.
        }

        // GET: Admin/Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);//id si eşleşen ilk kayıt sorgulanır
            if (blog == null)
            {
                return NotFound();//kayıt yoksa
            }

            return View(blog);//kayıt varsa
        }

        // GET: Admin/Blog/Create
        public IActionResult Create()
        {
            return View();//kullanıcıdan yeni bir blog kaydı oluşturması için
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)//sonucu bir görünüme döndermek için IActionResult ve kullanıcıdan gelen blog bilgileri
        {
            if (ModelState.IsValid) // erinin doğruluğunu konrol etmek
            {
                var files = HttpContext.Request.Form.Files;//Http isteği ilebirlikte gönderilen dosyaları almak
                                                           //files=yüklenen dosyaları içeren bir koleksiyon
                if (files.Count > 0)//kullanıcı herhangi bir dosya yüklemişse
                {
                    var fileName = Guid.NewGuid().ToString();//benzersiz bir ad oluşturma için Guid
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\menu");//dosyanın nereye kaydedileceği ve dosya uzantısı belirlemek
                                                                              // webrootpath = wwwroot klasörüne erişim sağlar,Path.Combine klasör yolu ve hedef dizinini birleştirir

                    var ext = Path.GetExtension(files[0].FileName);//yüklenen dosyanın uzanısını alır
                    if (blog.Image != null)//resim içeriyiyorsa
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, blog.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))//dosya var mı kontrol edilir
                        {
                            System.IO.File.Delete(imagePath);//dosay varsa silinir
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + ext), FileMode.Create))
                    //filestream ile hedef dosya yolu belirlenir  
                    //filemode.create = eğer dosya yoksa oluşturur varsa üzerine yazar
                    {
                        files[0].CopyTo(filesStreams);//yüklenen dosya filestreamse kopyalanır
                    }
                    blog.Image = @"\Site\menu\" + fileName + ext; // yüklenen dosyanın yolu blog.ımage e atanır
                }
                _context.Add(blog);//blog kaydı veritabanına yükenmek için işaretlenir
                await _context.SaveChangesAsync(); // asenkron bir biçimde kaydeder
                return RedirectToAction(nameof(Index)); // işlem bittiğinde ındex sayfasına yönlendirir
            }
            return View(blog);
        }

        // GET: Admin/Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)  //id null ise notfound
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id); //_context.Blogs veritabanındaki bloga erişir
                                                           //FindAsync(id) id değeriyle eşleşen blog kaydını asenkron olarak arar
                                                           //varsa blog değişkenine atanır

            //if (blog == null)
            //{
            //    return NotFound(); 
            //}
            return View(blog);
        }

        // POST: Admin/Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Name,Email,Image,Onay,Mesaj,Tarih")] Blog blog) //Bind sadecebelirtilen alanları alır
                                                                                                                      //Iactionresult  sonucu view a yönlendirmek için(Http isteğine yanıt verir)
        {
            if (id != blog.Id) //ıd alanı eşleşmiyorsa
            {
                return NotFound();
            }

            if (ModelState.IsValid) //verilerin doğruluğunu kontrol eder
            {
                try
                {
                    _context.Update(blog); // blog nesnesini günceller
                    await _context.SaveChangesAsync(); // asenkron olarak veritabanına kaydeder
                }
                catch (DbUpdateConcurrencyException)// güncelleme sırasında concurrency(eşzaman) ile ilgili bir hata meydana gelirse ele alır
                {
                    if (!BlogExists(blog.Id))//blog var mı kontrolü yapılır
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));//ındex sayfasına geri döner(bir Controller içindeki başka bir Action metoduna yönlendirme yapmak için kullanılır)
			}
            return View(blog);
        }

        // GET: Admin/Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/Blog/Delete/5
        [HttpPost, ActionName("Delete")] // delete metodu çağırılırken silme işlemi post ile yapılır
        [ValidateAntiForgeryToken]//CSRF saldırılarına karşı koruma 
		public async Task<IActionResult> DeleteConfirmed(int id)  
        {
            var blog = await _context.Blogs.FindAsync(id);//blog veritabaına erişir ve findsync ie id yi asenkron bir biçimde arar
            _context.Blogs.Remove(blog); // blog nesnesi silinmek üzere işaretler
            await _context.SaveChangesAsync(); // işaretlenen olayı veritabanında uygular
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id); //blog kaydı olup olmadıını kontrol eder
        }
    }
}
