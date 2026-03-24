using DatabaseMastery.HotCoffeePostgreSql.Services.ReservationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class AdminLayoutController : Controller
    {
        private readonly IReservationService _reservationService;

        public AdminLayoutController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ra = await _reservationService.ReservationCount();
            return View();
        }
    }
}
