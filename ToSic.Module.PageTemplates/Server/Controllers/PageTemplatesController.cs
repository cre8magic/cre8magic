using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oqtane.Controllers;
using Oqtane.Infrastructure;
using Oqtane.Shared;
using System.Threading.Tasks;
using ToSic.Module.PageTemplates.Services;
using ToSic.Module.PageTemplates.Shared;

namespace ToSic.Module.PageTemplates.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class PageTemplatesController(
        ServerPageTemplatesService serverPageTemplatesService,
        ILogManager logger,
        IHttpContextAccessor accessor)
        : ModuleControllerBase(logger, accessor)
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Task GetPageTemplates() => serverPageTemplatesService.CreateTestAsync();
    }
}
