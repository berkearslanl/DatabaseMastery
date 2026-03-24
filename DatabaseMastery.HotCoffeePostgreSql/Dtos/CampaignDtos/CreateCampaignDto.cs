namespace DatabaseMastery.HotCoffeePostgreSql.Dtos.CampaignDtos
{
    public class CreateCampaignDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
