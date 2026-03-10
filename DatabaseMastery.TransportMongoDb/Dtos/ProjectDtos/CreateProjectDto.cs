namespace DatabaseMastery.TransportMongoDb.Dtos.ProjectDtos
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
