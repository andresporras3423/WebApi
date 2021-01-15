using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class WordsController : Controller
    {
        // GET: Words
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool add_word(int users_id, int technos_id, string word, string translation)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                db.words.Add(new words { users_id = users_id, technos_id = technos_id, word = word, translation = translation });
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
        public bool update_word(int id, int technos_id, string word, string translation)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                words w = db.words.Find(id);
                w.technos_id = technos_id;
                w.word = word;
                w.translation = translation;
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

        public string get_words(int users_id, bool sort_by_word)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                List<string> ws = new List<string>();
                if (sort_by_word)
                {
                    ws = (from w in db.words
                                       where w.users_id == users_id
                                       orderby w.word
                                       select new wordModel{ id=w.id , 
                                                             users_id = w.users_id, 
                                                             technos_id = w.technos_id,
                                                             word = w.word,
                                                             translation = w.translation}).ToList<wordModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else
                {
                    ws = (from w in db.words
                                       where w.users_id == users_id
                                       orderby w.id
                                       select new wordModel
                                       {
                                           id = w.id,
                                           users_id = w.users_id,
                                           technos_id = w.technos_id,
                                           word = w.word,
                                           translation = w.translation
                                       }).ToList<wordModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(ws);
            }
        }

        public string search_words(int? users_id, int? technos_id, string word, string translation, int search)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                List<string> ws = new List<string>();
                if (search==0)
                {
                    ws = (from w in db.words
                          where w.users_id == users_id &&
                          (technos_id == null || w.technos_id == technos_id) &&
                          (word == null || w.word == word) &&
                          (translation == null || w.translation == translation)
                          select new wordModel
                          {
                              id = w.id,
                              users_id = w.users_id,
                              technos_id = w.technos_id,
                              word = w.word,
                              translation = w.translation
                          }).ToList<wordModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else if (search==1)
                {
                    ws = (from w in db.words
                          where w.users_id == users_id &&
                          (technos_id == null || w.technos_id == technos_id) &&
                          (word == null || w.word.Contains(word)) &&
                          (translation == null || w.translation.Contains(translation))
                          select new wordModel
                          {
                              id = w.id,
                              users_id = w.users_id,
                              technos_id = w.technos_id,
                              word = w.word,
                              translation = w.translation
                          }).ToList<wordModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else if (search==2)
                {
                    ws = (from w in db.words
                          where w.users_id == users_id &&
                          (technos_id == null || w.technos_id == technos_id) &&
                          (word == null || w.word.StartsWith(word)) &&
                          (translation == null || w.translation.StartsWith(translation))
                          select new wordModel
                          {
                              id = w.id,
                              users_id = w.users_id,
                              technos_id = w.technos_id,
                              word = w.word,
                              translation = w.translation
                          }).ToList<wordModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                else if (search == 3)
                {
                    ws = (from w in db.words
                          where w.users_id == users_id &&
                          (technos_id == null || w.technos_id == technos_id) &&
                          (word == null || w.word.EndsWith(word)) &&
                          (translation == null || w.translation.EndsWith(translation))
                          select new wordModel
                          {
                              id = w.id,
                              users_id = w.users_id,
                              technos_id = w.technos_id,
                              word = w.word,
                              translation = w.translation
                          }).ToList<wordModel>().
                                       Select(i => Newtonsoft.Json.JsonConvert.SerializeObject(i)).
                                       ToList<string>();
                }
                
                return Newtonsoft.Json.JsonConvert.SerializeObject(ws);
            }
        }
        [HttpDelete]
        public bool delete_word(int id)
        {
            using (english_projectEntities db = new english_projectEntities())
            {
                words w = db.words.Find(id);
                try
                {
                    db.words.Remove(w);
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