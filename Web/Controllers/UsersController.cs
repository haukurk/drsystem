using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.UI;
using Api.Models;
using Data;
using Data.Models;

namespace Api.Controllers
{
    public class UsersController : BaseApiController
    {
        public UsersController(IDRSRepository repository) : base(repository)
        {

        }

        [HttpGet]
        public IEnumerable<UserBaseModel> Get(int page = 0, int pageSize = 10)
        {
            IQueryable<User> query;

            query = DRSRepository.GetAllUsers().OrderBy(c => c.FirstName);

            var totalCount = query.Count();
            var totalPages = (int) Math.Ceiling((double) totalCount/pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("Users", new {page = page - 1, pageSize = pageSize}) : "";
            var nextLink = page < totalPages - 1
                ? urlHelper.Link("Users", new {page = page + 1, pageSize = pageSize})
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
            .Select(s => DRSModelFactory.CreateBasic(s));

            return results;

        }

        // TODO: THIS NEEDS PROTECTION.
        [HttpGet]
        public HttpResponseMessage Get(string username)
        {
            try
            {
                var user = DRSRepository.GetUser(username);
                if (user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(user));
                }

                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {

                // Set dates.
                user.RegistrationDate = DateTime.Now;
                user.LastLoginDate = null;

                DRSRepository.Insert(user);


                if (user == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error when parsing post body.");

                if (DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, DRSModelFactory.Create(user));
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save the user.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(string userName, [FromBody] User user)
        {
            try
            {
                var originalUser = DRSRepository.GetUser(userName);

                if (originalUser == null || originalUser.UserName == userName)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "User not found.");
                }

                user.UserId = originalUser.UserId;

                if (DRSRepository.Update(originalUser, user) && DRSRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(user));
                }

                return Request.CreateResponse(HttpStatusCode.NotModified);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string userName)
        {
            try
            {
                var user = DRSRepository.GetUser(userName);

                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (user.ReviewEntities.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        "Can not delete the users, it still has reviews entries.");
                }

                if (DRSRepository.DeleteUser(user.UserId) && DRSRepository.SaveAll())
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
