using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using ToSic.Module.Cre8magicTests.Repository;
using Oqtane.Controllers;
using System.Net;

namespace ToSic.Module.Cre8magicTests.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class Cre8magicTestsController : ModuleControllerBase
    {
        private readonly ICre8magicTestsRepository _Cre8magicTestsRepository;

        public Cre8magicTestsController(ICre8magicTestsRepository Cre8magicTestsRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _Cre8magicTestsRepository = Cre8magicTestsRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Cre8magicTests> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _Cre8magicTestsRepository.GetCre8magicTestss(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Cre8magicTests Get(int id)
        {
            Models.Cre8magicTests Cre8magicTests = _Cre8magicTestsRepository.GetCre8magicTests(id);
            if (Cre8magicTests != null && IsAuthorizedEntityId(EntityNames.Module, Cre8magicTests.ModuleId))
            {
                return Cre8magicTests;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Get Attempt {Cre8magicTestsId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Cre8magicTests Post([FromBody] Models.Cre8magicTests Cre8magicTests)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Cre8magicTests.ModuleId))
            {
                Cre8magicTests = _Cre8magicTestsRepository.AddCre8magicTests(Cre8magicTests);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Cre8magicTests Added {Cre8magicTests}", Cre8magicTests);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Post Attempt {Cre8magicTests}", Cre8magicTests);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Cre8magicTests = null;
            }
            return Cre8magicTests;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Cre8magicTests Put(int id, [FromBody] Models.Cre8magicTests Cre8magicTests)
        {
            if (ModelState.IsValid && Cre8magicTests.Cre8magicTestsId == id && IsAuthorizedEntityId(EntityNames.Module, Cre8magicTests.ModuleId) && _Cre8magicTestsRepository.GetCre8magicTests(Cre8magicTests.Cre8magicTestsId, false) != null)
            {
                Cre8magicTests = _Cre8magicTestsRepository.UpdateCre8magicTests(Cre8magicTests);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Cre8magicTests Updated {Cre8magicTests}", Cre8magicTests);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Put Attempt {Cre8magicTests}", Cre8magicTests);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Cre8magicTests = null;
            }
            return Cre8magicTests;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Cre8magicTests Cre8magicTests = _Cre8magicTestsRepository.GetCre8magicTests(id);
            if (Cre8magicTests != null && IsAuthorizedEntityId(EntityNames.Module, Cre8magicTests.ModuleId))
            {
                _Cre8magicTestsRepository.DeleteCre8magicTests(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Cre8magicTests Deleted {Cre8magicTestsId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Delete Attempt {Cre8magicTestsId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
