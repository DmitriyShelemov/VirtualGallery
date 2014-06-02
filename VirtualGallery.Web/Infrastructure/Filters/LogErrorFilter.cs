using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VirtualGallery.Infrastructure.Logging;

namespace VirtualGallery.Web.Infrastructure.Filters
{
    public class LogErrorFilter : IExceptionFilter
    {
        /// <summary>
        ///   Logs exception
        /// </summary>
        /// <param name="context"> </param>
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            OnException(ex);        
        }

        /// <summary>
        /// Log exception coming without MVC context.
        /// </summary>
        /// <param name="ex"></param>
        public void OnException(Exception ex)
        {
            int code = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500;

            // Skip 404
            if (code == (int)HttpStatusCode.NotFound)
                return;

            Logger.Instance.WriteLog(ex.ToString(), LogLevel.Error);
        }
    }
}