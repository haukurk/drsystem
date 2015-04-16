using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Api.Controllers;
using Api.Filters;
using Api.Models;
using Data;
using Data.Models;

namespace API.Controllers
{
    public class LogsController : BaseApiController
    {
        public LogsController(IDRSRepository repository) : base(repository)
        {
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpGet]
        public HttpResponseMessage Get(int logId)
        {
            try
            {
                var log = DRSRepository.GetLog(logId);
                if (log != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(log));
                }

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpGet]
        public IEnumerable<LogModel> Get(int page = 0, int pageSize = 50)
        {
            IQueryable<Log> query = DRSRepository.GetAllLogs().OrderBy(l => l.Created);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("Logs", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1
                ? urlHelper.Link("Logs", new { page = page + 1, pageSize = pageSize })
                : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination",
                                                                 Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


            var results = query
            .Skip(pageSize * page)
            .Take(pageSize)
            .ToList()
            .Select(s => DRSModelFactory.Create(s));

            return results;
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] LogModel logModel)
        {
            try
            {

                var log = DRSModelFactory.Parse(logModel);

                DRSRepository.Insert(log);

                if (log == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error when parsing post body.");

                var saveStatus = DRSRepository.SaveAllWithValidation();

                if (saveStatus.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, DRSModelFactory.Create(log));
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, saveStatus);
            }
            catch (DbEntityValidationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.EntityValidationErrors.First().ValidationErrors);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(int logid, [FromBody] LogModel log)
        {
            try
            {
                var originalLog = DRSRepository.GetLog(logid);
                var updatedLog = DRSModelFactory.Parse(log);

                if (originalLog == null || originalLog.Id != logid)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Log not found.");
                }

                updatedLog.Id = originalLog.Id;

                if (DRSRepository.Update(originalLog, updatedLog) && DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(updatedLog));
                }

                return Request.CreateResponse(HttpStatusCode.NotModified);

            }
            catch (DbEntityValidationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.EntityValidationErrors.First().ValidationErrors);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // TODO: Add admin role.
        [DRSBasicAuthorization(active: true, ownerRestricted: false)]
        [HttpDelete]
        public HttpResponseMessage Delete(int logid)
        {
            try
            {
                var log = DRSRepository.GetLog(logid);

                if (log == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (DRSRepository.DeleteLog(log.Id) && DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

    }
}
