using Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http;
using Api.Models;
using Data;
using Data.Models;

namespace API.Controllers
{
    public class IssuesController : BaseApiController
    {
        public IssuesController(IDRSRepository repository) : base(repository)
        {

        }

        [HttpGet]
        public IEnumerable<IssueModel> Get(int page = 0, int pageSize = 50)
        {
            IQueryable<Issue> query = DRSRepository.GetAllIssues().OrderBy(l => l.ReviewEntity.System.Name);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("Issues", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1
                ? urlHelper.Link("Issues", new { page = page + 1, pageSize = pageSize })
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

        [HttpGet]
        public HttpResponseMessage Get(int issueid)
        {
            try
            {
                var issue = DRSRepository.GetIssue(issueid);
                if (issue != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(issue));
                }

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
