using DatabaseMastery.HotCoffeePostgreSql.Dtos.ReservationDtos;
using DatabaseMastery.HotCoffeePostgreSql.Services.ReservationServices;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace DatabaseMastery.HotCoffeePostgreSql.Controllers
{
    public class PublicReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public PublicReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Phone) ||
                dto.GuestCount < 1 ||
                dto.ReservationDate == default)
            {
                return Json(new { success = false, message = "Lütfen tüm zorunlu alanları doldurun." });
            }

           
            dto.Status = "Beklemede";
            dto.ReservationDate = DateTime.SpecifyKind(dto.ReservationDate, DateTimeKind.Utc);

            await _reservationService.CreateReservationAsync(dto);

            #region MailGönderme

            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("DINER", "berkesude39@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", dto.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = $"Rezervasyonunuz Alındı — Lezzet Bahçesi";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Merhaba {dto.Name},\r\n\r\nRezervasyon talebiniz başarıyla alınmıştır.\r\n\r\nRezervasyon Detayları:\r\nTarih: {dto.ReservationDate.ToString("dd MMMM yyyy")}\r\nSaat: {dto.ReservationTime}\r\nKişi Sayısı: {dto.GuestCount}\r\n\r\nDurum: Beklemede\r\nRezervasyon onaylandığında size bilgi verilecektir.\r\n\r\nDeğişiklik veya iptal için bizi arayabilirsiniz:\r\n+90 288 412 36 54\r\n\r\nSizi ağırlamak için sabırsızlanıyoruz!\r\n\r\nLezzet Bahçesi Ekibi\r\nBağdat Caddesi No: 123, Kadıköy / İstanbul"
;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("berkesude39@gmail.com", "jufk mzgc gmlt wjpq");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            #endregion

            return Json(new { success = true, message = "Rezervasyonunuz başarıyla alındı! En kısa sürede size dönüş yapacağız." });
        }
    }
}
