using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Shared;

namespace ToSic.Util.OqtaneTheme2TemplateApp
{
    internal class UtilLogManager : ILogManager
    {
        public void Log(LogLevel level, object @class, LogFunction function, string message, params object[] args) 
            => LogInternal(null, level, @class, function, null, message, args);

        public void Log(LogLevel level, object @class, LogFunction function, Exception exception, string message, params object[] args)
            => LogInternal(null, level, @class, function, exception, message, args);

        public void Log(int siteId, LogLevel level, object @class, LogFunction function, string message, params object[] args) 
            => LogInternal(siteId, level, @class, function, null, message, args);

        public void Log(int siteId, LogLevel level, object @class, LogFunction function, Exception exception, string message, params object[] args)
            => LogInternal(siteId, level, @class, function, exception, message, args);

        public void Log(Log log) 
            => Console.WriteLine($"LogId: {log.LogId}, SiteId: {log.SiteId}, LogDate: {log.LogDate}, PageId: {log.PageId}, ModuleId: {log.ModuleId}, UserId: {log.UserId}, Url: {log.Url}, Server: {log.Server}, Category: {log.Category}, Feature: {log.Feature}, Function: {log.Function}, Level: {log.Level}, Message: {log.Message}");

        private void LogInternal(int? siteId, LogLevel level, object @class, LogFunction function, Exception exception, string message, params object[] args)
        {
            var log = new Log
            {
                SiteId = siteId,
                Level = level.ToString(),
                Category = @class.GetType().Name,
                Function = function.ToString(),
                Message = string.Format(message, args),
                Exception = exception?.ToString()
            };
            Log(log);
        }
    }
}
