using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Security;
using Data;
using Ninject;

namespace Api.Filters
{
    /// <summary>
    /// Generic Basic Authentication filter that checks for basic authentication
    /// headers and challenges for authentication if no authentication is provided
    /// Sets the Thread Principle with a GenericAuthenticationPrincipal.
    /// 
    /// You can override the OnAuthorize method for custom auth logic that
    /// might be application specific.    
    /// </summary>
    /// <remarks>Always remember that Basic Authentication passes username and passwords
    /// from client to server in plain text, so make sure SSL is used with basic auth
    /// to encode the Authorization header on all requests (not just the login).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class DRSBasicAuthorizationAttribute : AuthorizationFilterAttribute
    {

        [Inject]
        public DRSRepository Repo { get; set; }

        bool Active = true;
        bool OwnerRestricted = true;

        public DRSBasicAuthorizationAttribute()
        { }

        /// <summary>
        /// Overriden constructor to allow explicit disabling of this
        /// filter's behavior. Pass false to disable (same as no filter
        /// but declarative)
        /// </summary>
        /// <param name="active"></param>
        public DRSBasicAuthorizationAttribute(bool active, bool ownerRestricted=false)
        {
            Active = active;
            OwnerRestricted = ownerRestricted;
        }

        /// <summary>
        /// Override to Web API filter method to handle Basic Auth check
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Active)
            {
                var identity = ParseAuthorizationHeader(actionContext);
                if (identity == null)
                {
                    Challenge(actionContext);
                    return;
                }


                if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
                {
                    Challenge(actionContext);
                    return;
                }

                if (OwnerRestricted)
                {
                    if (!IsResourceOwner(identity.Name, actionContext))
                    {
                        Challenge(actionContext);
                        return;
                    }
                }

                var principal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = principal;

                // inside of ASP.NET this is required
                //if (HttpContext.Current != null)
                //    HttpContext.Current.User = principal;

                base.OnAuthorization(actionContext);
            }
        }


        /// <summary>
        /// Base implementation for user authentication.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            /*if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            */

            if (Repo.LoginUser(username, password))
                return true;

            return false;
        }

        /// <summary>
        /// Parses the Authorization header and creates user credentials
        /// </summary>
        /// <param name="actionContext"></param>
        protected virtual BasicAuthenticationIdentity ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            string authHeader = null;
            var auth = actionContext.Request.Headers.Authorization;
            if (auth != null && auth.Scheme == "Basic")
                authHeader = auth.Parameter;

            if (string.IsNullOrEmpty(authHeader))
                return null;

            authHeader = Encoding.Default.GetString(Convert.FromBase64String(authHeader));

            var tokens = authHeader.Split(':');
            if (tokens.Length < 2)
                return null;

            return new BasicAuthenticationIdentity(tokens[0], tokens[1]);
        }


        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionContext"></param>
        void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Not allowed, or wrong credentials.");
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
        }

        private bool IsResourceOwner(string userName, System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                var routeData = actionContext.Request.GetRouteData();
                var resourceUserName = routeData.Values["userName"] as string;

                if (resourceUserName == userName)
                {
                    return true;
                }
            }
            catch
            {
                DRSLogger.Instance.Error("Error trying to find owner of the resouces. Missing username.");
            }

            return false;
            
        }

    }
}