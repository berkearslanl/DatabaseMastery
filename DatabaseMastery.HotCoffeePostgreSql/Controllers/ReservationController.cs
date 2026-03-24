using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReservationDtos;
using DatabaseMastery.HotCoffeePostgreSql.Services.ActivityLogServices;
using DatabaseMastery.HotCoffeePostgreSql.Services.ReservationServices;
using Humanizer;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _ReservationService;
        private readonly IActivityLogService _ActivityLogService;

        public ReservationController(IReservationService ReservationService, IActivityLogService activityLogService)
        {
            _ReservationService = ReservationService;
            _ActivityLogService = activityLogService;
        }

        public async Task<IActionResult> ReservationList()
        {
            var values = await _ReservationService.GetAllReservationsAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateReservation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            createReservationDto.ReservationDate = DateTime.SpecifyKind(createReservationDto.ReservationDate, DateTimeKind.Utc);
            await _ReservationService.CreateReservationAsync(createReservationDto);
            _ActivityLogService.LogActivity
                (
                "Yeni rezervasyon oluşturuldu",
                $"{createReservationDto.Name} - {createReservationDto.GuestCount} kişi, {createReservationDto.ReservationTime}",
                "bi bi-calendar2-check-fill",
                "var(--green)",
                "var(--green-soft)"
                );
            return RedirectToAction("ReservationList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int id)
        {
            var values = await _ReservationService.GetReservationByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            updateReservationDto.ReservationDate = DateTime.SpecifyKind(updateReservationDto.ReservationDate, DateTimeKind.Utc);
            await _ReservationService.UpdateReservationAsync(updateReservationDto);
            _ActivityLogService.LogActivity(
                "Rezervasyon güncellendi",
                $"{updateReservationDto.Name} — {updateReservationDto.GuestCount} kişi, {updateReservationDto.ReservationTime}",
                "bi bi-pencil-square",
                "var(--blue)",
                "var(--blue-soft)"
            );
            return RedirectToAction("ReservationList");
        }
        public async Task<IActionResult> DeleteReservation(int id, string returnUrl)
        {
            await _ReservationService.DeleteReservationAsync(id);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("ReservationList");
        }
        public async Task<IActionResult> CancelReservation(int id, string returnUrl)
        {
            var value = await _ReservationService.GetReservationByIdAsync(id);
            await _ReservationService.ChangeReservationStatusToCancel(id);
            _ActivityLogService.LogActivity(
                "Rezervasyon iptal edildi",
                $"#{id} numaralı rezervasyon iptal edildi",
                "bi bi-x-circle-fill",
                "var(--red)",
                "var(--red-soft)"
            );

            #region MailGönderme

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("DINER", "berkesude39@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", value.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = $"Rezervasyonunuz İptal Edildi — Lezzet Bahçesi";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Merhaba {value.Name},\r\n\r\nRezervasyon talebiniz iptal edilmiştir.\r\n\r\nİptal Edilen Rezervasyon:\r\nTarih: {value.ReservationDate.ToString("dd MMMM yyyy")}\r\nSaat: {value.ReservationTime}\r\nKişi Sayısı: {value.GuestCount}\r\n\r\nYeni bir rezervasyon oluşturmak isterseniz web sitemizi ziyaret edebilir veya bizi arayabilirsiniz:\r\n+90 288 412 36 54\r\n\r\nTekrar görüşmek dileğiyle,\r\n\r\nLezzet Bahçesi Ekibi\r\nBağdat Caddesi No: 123, Kadıköy / İstanbul"


;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("berkesude39@gmail.com", "jufk mzgc gmlt wjpq");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            #endregion

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            

            return RedirectToAction("ReservationList");
        }
        public async Task<IActionResult> PendingReservation(int id, string returnUrl)
        {
            await _ReservationService.ChangeReservationStatusToPending(id);
            _ActivityLogService.LogActivity(
                "Rezervasyon beklemeye alındı",
                $"#{id} numaralı rezervasyon beklemeye alındı",
                "bi bi-clock-fill",
                "var(--yellow)",
                "var(--yellow-soft)"
            );
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("ReservationList");
        }
        public async Task<IActionResult> ApproveReservation(int id, string returnUrl)
        {
            var value = await _ReservationService.GetReservationByIdAsync(id);
            await _ReservationService.ChangeReservationStatusToApprove(id);
            _ActivityLogService.LogActivity(
                "Rezervasyon onaylandı",
                $"#{id} numaralı rezervasyon onaylandı",
                "bi bi-check-circle-fill",
                "var(--green)",
                "var(--green-soft)"
            );

            #region MailGönderme

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("DINER", "berkesude39@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", value.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = $"Rezervasyonunuz Onaylandı — Lezzet Bahçesi";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Merhaba {value.Name},\r\n\r\nRezervasyon talebiniz onaylanmıştır!\r\n\r\nRezervasyon Detayları:\r\nTarih: {value.ReservationDate.ToString("dd MMMM yyyy")}\r\nSaat: {value.ReservationTime}\r\nKişi Sayısı: {value.GuestCount}\r\n\r\nSizi belirtilen tarih ve saatte restoranımızda ağırlamaktan mutluluk duyacağız.\r\nHerhangi bir değişiklik için bizi arayabilirsiniz:\r\n+90 288 412 36 54\r\n\r\nAfiyet olsun!\r\n\r\nLezzet Bahçesi Ekibi\r\nBağdat Caddesi No: 123, Kadıköy / İstanbul"

;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("berkesude39@gmail.com", "jufk mzgc gmlt wjpq");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            #endregion

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            

            return RedirectToAction("ReservationList");
        }
    }
}
