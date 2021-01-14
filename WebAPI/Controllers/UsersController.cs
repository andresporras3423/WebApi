using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            
            return View();
        }

        //[System.Web.Http.Route("api/users")]
        //[System.Web.Http.HttpGet]
        public string Get()
        {
            IQueryable<users> usuarios;
            List<string> lista = new List<String>();
            using (english_projectEntities db = new english_projectEntities())
            {
                usuarios = db.users;
                foreach (var item in usuarios)
                {
                    lista.Add($"Usuario: {item.username}, email: {item.email}");
                }
            }
            return lista[0];
        }

        public bool find_user(string email, string pass)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                bool user_found = db.users.Any(us => us.email == email && us.pass == pass);
                return user_found;
            }
        }

        [HttpPost]
        public bool add_user(string username, string email, string pass)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                users x = db.users.Add(new users { username = username, email = email, pass = pass });
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
    }
}