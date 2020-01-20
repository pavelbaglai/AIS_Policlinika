using System;
using System.Web;
using System.Web.Mvc;
using DigitalMedicine.Models;
using DigitalMedicine.Models.Users;

namespace DigitalMedicine.Filters
{
    public class MyAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = (User)httpContext.Session["currentUser"];
            if (user == null)
                return false;
            string[] idRolesString = base.Roles.Split(',');
            for (int i = 0; i < idRolesString.Length; i++)
                if (int.Parse(idRolesString[i].Trim()) == user.IdRole)
                    return true;
            return false;
        }
    }
}