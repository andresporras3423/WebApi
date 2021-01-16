using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

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

        public string get_tests(int users_id)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                List<string> user_tests = new List<string>();
                user_tests = (from ts in db.tests
                      where ts.users_id == users_id
                      orderby ts.id
                      select new testModel
                      {
                          id = ts.id,
                          users_id = ts.users_id,
                          correct = ts.correct,
                          total = ts.total,
                          created_datetime = ts.created_datetime
                      }).ToList<testModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                return Newtonsoft.Json.JsonConvert.SerializeObject(user_tests);
            }
        }
    }
}