using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using DigitalMedicine.Models;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.Filters
{
    public class MyAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            User user = (User)filterContext.HttpContext.Session["currentUser"];
            if(user==null)
                filterContext.Result= new HttpUnauthorizedResult("Вы не авторизованы в системе!");
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            User user = (User)filterContext.HttpContext.Session["currentUser"];
            if(user==null)
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Home" }, { "action", "Authorization" }
                   });
        }
    }
}