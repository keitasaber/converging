using Converging.Model.Models;
using Converging.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Converging.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        IErrorService _errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = function.Invoke();
            }
            catch (DbEntityValidationException dbEvx)
            {
                foreach (var eve in dbEvx.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \" {eve.Entry.Entity.GetType().Name} \" in state \"{eve.Entry.State} \" has the following validation errors ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \" {ve.PropertyName} \", Error: \"{ve.ErrorMessage} \"");
                    }
                    LogError(dbEvx);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEvx.InnerException.Message);
                }
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return responseMessage;
        }

        // get error is created
        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.save();
            }
            catch
            {

            }
        }
    }
}