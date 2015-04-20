using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Controllers;
using Api.Filters;
using Data;
using Data.Models;

namespace API.Controllers
{
    public class AuthorizationController : BaseApiController
    {
        public AuthorizationController(IDRSRepository repository) : base(repository)
        {
        }


        [DRSBasicAuthorization(active: false, ownerRestricted: false)]
        [HttpPost]
        public HttpResponseMessage Post(string method, [FromBody] User user)
        {

            if (method == "login")
            {

                try
                {

                    if (user == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error when parsing post body.");

                    if (!DRSRepository.LoginUser(user.UserName, user.Password))
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "Wrong username or password.");

                    var modelUser = DRSRepository.GetUser(user.UserName);
                    modelUser.LastLoginDate = DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, DRSModelFactory.Create(modelUser));
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }

            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Auth method not found.");
        }
    }
}
