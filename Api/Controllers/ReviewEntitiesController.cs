using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Models;
using Data;
using Data.Helpers;
using Data.Models;
using System.Web.Http.Routing;

namespace Api.Controllers
{
    public class ReviewEntitiesController : BaseApiController
    {
        public ReviewEntitiesController(IDRSRepository repository) : base(repository)
        {
        }

        [HttpGet]
        public IEnumerable<ReviewEntityModel> Get(int page = 0, int pageSize = 50)
        {
            IQueryable<ReviewEntity> query;

            query = DRSRepository.GetAllReviewsEntities().OrderBy(c => c.System.Name);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("Users", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1
                ? urlHelper.Link("Users", new { page = page + 1, pageSize = pageSize })
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
        public HttpResponseMessage Get(int reviewId)
        {
            try
            {
                var review = DRSRepository.GetReviewEntity(reviewId);
                if (review != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(review));
                }

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] ReviewEntityModel reviewEntity)
        {
            try
            {

                var review = DRSModelFactory.Parse(reviewEntity);

                DRSRepository.Insert(review);

                if (reviewEntity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error when parsing post body.");

                var saveStatus = DRSRepository.SaveAllWithValidation();

                if (saveStatus.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, DRSModelFactory.Create(review));
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

    }
}
