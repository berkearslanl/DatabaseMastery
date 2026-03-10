using DatabaseMastery.TransportMongoDb.Services.ProjectServices;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMastery.TransportMongoDb.ViewComponents.Default
{
    public class _DefaultProjectComponentPartial:ViewComponent
    {
        private readonly IProjectService _projectService;

        public _DefaultProjectComponentPartial(IProjectService ProjectService)
        {
            _projectService = ProjectService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _projectService.GetAllProjectAsync();
            return View(values);
        }
    }
}
