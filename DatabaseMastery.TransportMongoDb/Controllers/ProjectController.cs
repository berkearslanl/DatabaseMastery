using DatabaseMastery.TransportMongoDb.Dtos.ProjectDtos;
using DatabaseMastery.TransportMongoDb.Services.ProjectServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService ProjectService)
        {
            _projectService = ProjectService;
        }

        public async Task<IActionResult> ProjectList()
        {
            var values = await _projectService.GetAllProjectAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
        {
            await _projectService.CreateProjectAsync(createProjectDto);
            return RedirectToAction("ProjectList");
        }
        public async Task<IActionResult> DeleteProject(string id)
        {
            await _projectService.DeleteProject(id);
            return RedirectToAction("ProjectList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProject(string id)
        {
            var value = await _projectService.GetProjectByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject(UpdateProjectDto updateProjectDto)
        {
            await _projectService.UpdateProjectAsync(updateProjectDto);
            return RedirectToAction("ProjectList");
        }
    }
}
