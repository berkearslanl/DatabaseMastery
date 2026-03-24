using DatabaseMastery.HotCoffeePostgreSql.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.AdminUsers.FirstOrDefaultAsync();
            ViewBag.Username = user?.Username;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCredentials(string username, string currentPassword, string newPassword)
        {
            var user = await _context.AdminUsers.FirstOrDefaultAsync();

            if (user == null || user.Password != currentPassword)
            {
                TempData["CredentialError"] = "Mevcut şifre hatalı!";
                return RedirectToAction("Index");
            }

            if (!string.IsNullOrWhiteSpace(username))
                user.Username = username;

            if (!string.IsNullOrWhiteSpace(newPassword))
                user.Password = newPassword;

            await _context.SaveChangesAsync();
            TempData["CredentialSuccess"] = "Bilgiler başarıyla güncellendi!";
            return RedirectToAction("Index");
        }
    }
}
