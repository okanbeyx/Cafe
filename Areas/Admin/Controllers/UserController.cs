using Cafe.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context; // veritbanı bağlama işlemi
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.ApplicationUsers.ToList(); //tüm kullanıcıları alıp bir liste hline getirmesi
            var role = _context.Roles.ToList();//tm rollerin listesini alır
            var userRol = _context.UserRoles.ToList();//kullanıcı-rol ilişkisini alır
            foreach (var item in users)//kullanıcılar üzeriden dönülür
            {
                var roleId = userRol.FirstOrDefault(i => i.UserId == item.Id).RoleId;// her kullanıcı için kullanıcının rolünü belirler
																					 // user rol tablosundaki ilişkiden kullanıcıya ait roleıd yi alır
																					 //FirstOrDefault koşul belirtilmezse ilk elemanı alır
				item.Role = role.FirstOrDefault(u => u.Id == roleId).Name; // rol adını bulur ve kullanıcının role alanına atar
            }
            return View(users);
        }
        public async Task<IActionResult> Delete(string id) //asenkron bir şekilde silme işlemi yapmak için
														   //Task işlemin sonucunu beirler IActionResult döndürür
														   //IActionResult aksiyonmetodunun HTTP cevabı
		{
			if (id == null)// kullanıcı bulunamazsa
            {
                return NotFound();
            }

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id.ToString()); //aplicationusers tablosundaki id ye sahip
                                                                  //kullanıcıyı asenkron olarak tarar kullanıcı bulunursa user değişkenini atar
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")] // kullanıcı onayladıktan sonra çalışır
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);//applicationusers tablosunda asenkron olarak arar ve bulur
            _context.ApplicationUsers.Remove(user);//bulunan kullanıcıyı veritabanından siler
            await _context.SaveChangesAsync();//veritabnına kaydedilmesini sağlar
            return RedirectToAction(nameof(Index));//silindikten sorna kullanıcıyı ındex e yönlendirir
        }
    }
}
