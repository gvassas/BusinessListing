using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class UserRepository
    {
        public DBUser CheckLogin(string username, string password)
        {
            BusinessListingsEntities db = new BusinessListingsEntities();
            var login = db.DBUsers.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
            if (login != null)
            {
                HttpContext.Current.Session["Log"] = "true";
            }
            return login;
        }

        public void Logout()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            
        }
    }
}