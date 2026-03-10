namespace DatabaseMastery.TransportMongoDb.Dtos.ProjectDtos
{
    public class UpdateProjectDto
    {
        public string ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
