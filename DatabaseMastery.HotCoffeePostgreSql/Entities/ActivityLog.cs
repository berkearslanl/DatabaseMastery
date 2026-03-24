namespace DatabaseMastery.HotCoffeePostgreSql.Entities
{
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public string Title { get; set; }          // "Yeni rezervasyon oluşturuldu"
        public string Description { get; set; }    // "Ahmet Yılmaz — 4 kişi, 19:30"
        public string Icon { get; set; }           // "bi bi-calendar2-check-fill"
        public string IconColor { get; set; }      // "var(--green)"
        public string IconBackground { get; set; } // "var(--green-soft)"
        public DateTime CreatedAt { get; set; }    // İşlem zamanı
    }
}
