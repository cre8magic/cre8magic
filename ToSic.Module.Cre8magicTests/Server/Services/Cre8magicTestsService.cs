using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using ToSic.Module.Cre8magicTests.Repository;

namespace ToSic.Module.Cre8magicTests.Services
{
    public class ServerCre8magicTestsService : ICre8magicTestsService
    {
        private readonly ICre8magicTestsRepository _Cre8magicTestsRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerCre8magicTestsService(ICre8magicTestsRepository Cre8magicTestsRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _Cre8magicTestsRepository = Cre8magicTestsRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Cre8magicTests>> GetCre8magicTestssAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_Cre8magicTestsRepository.GetCre8magicTestss(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Cre8magicTests> GetCre8magicTestsAsync(int Cre8magicTestsId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_Cre8magicTestsRepository.GetCre8magicTests(Cre8magicTestsId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Get Attempt {Cre8magicTestsId} {ModuleId}", Cre8magicTestsId, ModuleId);
                return null;
            }
        }

        public Task<Models.Cre8magicTests> AddCre8magicTestsAsync(Models.Cre8magicTests Cre8magicTests)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Cre8magicTests.ModuleId, PermissionNames.Edit))
            {
                Cre8magicTests = _Cre8magicTestsRepository.AddCre8magicTests(Cre8magicTests);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Cre8magicTests Added {Cre8magicTests}", Cre8magicTests);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Add Attempt {Cre8magicTests}", Cre8magicTests);
                Cre8magicTests = null;
            }
            return Task.FromResult(Cre8magicTests);
        }

        public Task<Models.Cre8magicTests> UpdateCre8magicTestsAsync(Models.Cre8magicTests Cre8magicTests)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Cre8magicTests.ModuleId, PermissionNames.Edit))
            {
                Cre8magicTests = _Cre8magicTestsRepository.UpdateCre8magicTests(Cre8magicTests);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Cre8magicTests Updated {Cre8magicTests}", Cre8magicTests);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Update Attempt {Cre8magicTests}", Cre8magicTests);
                Cre8magicTests = null;
            }
            return Task.FromResult(Cre8magicTests);
        }

        public Task DeleteCre8magicTestsAsync(int Cre8magicTestsId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _Cre8magicTestsRepository.DeleteCre8magicTests(Cre8magicTestsId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Cre8magicTests Deleted {Cre8magicTestsId}", Cre8magicTestsId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Cre8magicTests Delete Attempt {Cre8magicTestsId} {ModuleId}", Cre8magicTestsId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
