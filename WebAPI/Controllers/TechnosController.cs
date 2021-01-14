using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class TechnosController : Controller
    {
        // GET: Technos
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool add_techno(string techno_name, bool techno_status, int users_id)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                technos x = db.technos.Add(new technos { techno_name = techno_name, techno_status = techno_status, users_id = users_id});
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