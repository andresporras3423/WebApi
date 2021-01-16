using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class TestsController : Controller
    {
        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool add_test(int users_id, int correct, int total)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                tests x = db.tests.Add(new tests { users_id = users_id, correct = correct, total = total, created_datetime= DateTime.Now });
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