using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafe.Data;
using Cafe.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cafe.Areas.Admin.Controllers
{
    [Area("Admin")] //Url yapısı admin about sayfasına yönlendirmek için
    [Authorize] //sadece yetkili kullanıcılar için
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;//veritabanı bağlantısını yöneten sınıf

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/About
        public async Task<IActionResult> Index()// abou tablosudaki tüm kayıtları veritabanından viewa gönderir
        {
            return View(await _context.Abouts.ToListAsync());//senkronize bir şekilde kayıtları çeker  async: asenkron olarak tanmlar
        }

        // GET: Admin/About/Details/5
        public async Task<IActionResult> Details(int? id)//görüntülenmek istenen about kaydının id si
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Id == id);//belirtilen id ye sahip ilk kayıt
            if (about == null)
            {
                return NotFound();
            }

            return View(about);//bulunan kayıt view'a gönderilir
        }

        // GET: Admin/About/Create
        public IActionResult Create()
        {
            return View();//yeni kayıt oluşturmak için form sayfasına döner
        }

        // POST: Admin/About/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//formdan veri gönderdiğinde çalşır
        [ValidateAntiForgeryToken]//csrf saldırıları için güvenlik
        public async Task<IActionResult> Create([Bind("Id,Title")] About about)//sadece belirtilen alanların modelde doldurulmasına izin verir
        {
            if (ModelState.IsValid)//gönderilen verilerin doğru olup olmadığını kontrol eder
            {
                _context.Add(about);//yeni kaydı veritabanına ekler
                await _context.SaveChangesAsync();//veritabanına değişiklikleri kaydeder
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: Admin/About/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts.FindAsync(id);//veritabanında belirtilen ıd ye sahip kaydı getirir
            if (about == null)
            {
                return NotFound();//kayıt bulunmazsa 404
            }
            return View(about);//buluursa about sayfasına döner
        }

        // POST: Admin/About/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] About about)//task asenkron işlemini temsil eder
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(about);//kayıt güncellenir
                    await _context.SaveChangesAsync();//bir asenkron işlemin sonucunu bekler ardıan kaydeder
                }
                catch (DbUpdateConcurrencyException)//güncelleme sırasında başka bir işlem kaydı değiştirmişse hata yakalanır
                {
                    if (!AboutExists(about.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // hata fırlatır
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: Admin/About/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts  //_context= veri tabanına bağlamak için,Abouts vritabanındaki about sayfasını temsil eder
                .FirstOrDefaultAsync(m => m.Id == id);//tablo içindeki kayıtları sorgular ve koşulu sağlayan ilk kaydı döndürür
            //await burda işlemin bitmesini bekler bittikten sonra sonucu about değişkenine atar.
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }//silmeden önce kulanıcıya silmek istediğinizden emin misiniz? onay ekranını gösterir

        // POST: Admin/About/Delete/5
        [HttpPost, ActionName("Delete")]//silme işlemini onaylamak için
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            _context.Abouts.Remove(about);//kaydı silmek için
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));//değişiklikler kaydedilir ve index sayfasına dönderilir
        }

        private bool AboutExists(int id)
        {
            return _context.Abouts.Any(e => e.Id == id);//kayıt var mı yok mu diye kontrol yapar
        }
    }
}
