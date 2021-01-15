using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

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

        [HttpPut]
        public bool update_techno(int id, string techno_name, bool techno_status, int users_id)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                technos t = db.technos.Find(id);
                t.techno_name = techno_name;
                t.techno_status = techno_status;
                t.users_id = users_id;
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

        public string get_technos(int users_id, bool sort_by_name)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                List<string> ts = new List<string>();
                if (sort_by_name)
                {
                    ts = (from t in db.technos
                          where t.users_id == users_id
                          orderby t.techno_name
                          select new technoModel
                          {
                              id = t.id,
                              users_id = t.users_id,
                              techno_name = t.techno_name,
                              techno_status = t.techno_status
                          }).ToList<technoModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else
                {
                    ts = (from t in db.technos
                          where t.users_id == users_id
                          orderby t.id
                          select new technoModel
                          {
                              id = t.id,
                              users_id = t.users_id,
                              techno_name = t.techno_name,
                              techno_status = t.techno_status
                          }).ToList<technoModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(ts);
            }
        }

        public string search_technos(string techno_name, bool? techno_status, int users_id, int search)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                List<string> ts = new List<string>();
                if (search == 0)
                {
                    ts = (from t in db.technos
                          where t.users_id == users_id &&
                          (techno_name == null || t.techno_name == techno_name) &&
                          (techno_status == null || t.techno_status == techno_status)
                          select new technoModel
                          {
                              id = t.id,
                              users_id = t.users_id,
                              techno_name = t.techno_name,
                              techno_status = t.techno_status
                          }).ToList<technoModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else if (search == 1)
                {
                    ts = (from t in db.technos
                          where t.users_id == users_id &&
                          (techno_name == null || t.techno_name.Contains(techno_name)) &&
                          (techno_status == null || t.techno_status == techno_status)
                          select new technoModel
                          {
                              id = t.id,
                              users_id = t.users_id,
                              techno_name = t.techno_name,
                              techno_status = t.techno_status
                          }).ToList<technoModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else if (search == 2)
                {
                    ts = (from t in db.technos
                          where t.users_id == users_id &&
                          (techno_name == null || t.techno_name.StartsWith(techno_name)) &&
                          (techno_status == null || t.techno_status == techno_status)
                          select new technoModel
                          {
                              id = t.id,
                              users_id = t.users_id,
                              techno_name = t.techno_name,
                              techno_status = t.techno_status
                          }).ToList<technoModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else if (search == 3)
                {
                    ts = (from t in db.technos
                          where t.users_id == users_id &&
                          (techno_name == null || t.techno_name.EndsWith(techno_name)) &&
                          (techno_status == null || t.techno_status == techno_status)
                          select new technoModel
                          {
                              id = t.id,
                              users_id = t.users_id,
                              techno_name = t.techno_name,
                              techno_status = t.techno_status
                          }).ToList<technoModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(ts);
            }
        }

        [HttpDelete]
        public bool delete_techno(int id)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                technos t = db.technos.Find(id);
                try
                {
                    db.technos.Remove(t);
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