using DatabaseMastery.TransportMongoDb.Dtos.ProjectDtos;

namespace DatabaseMastery.TransportMongoDb.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<List<ResultProjectDto>> GetAllProjectAsync();
        Task CreateProjectAsync(CreateProjectDto createProjectDto);
        Task UpdateProjectAsync(UpdateProjectDto updateProjectDto);
        Task<GetProjectByIdDto> GetProjectByIdAsync(string id);
        Task DeleteProject(string id);
    }
}
