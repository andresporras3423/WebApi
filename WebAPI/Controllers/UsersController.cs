using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class UsersController : Controller
    {
        static HttpClient client = new HttpClient();
        public async System.Threading.Tasks.Task<JObject> Index()
        {
            string product;
            JObject p = new JObject();
            using (HttpResponseMessage response = await client.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=bogota&appid=a71219e79a6b01978ac3a9f3ffccca37"))
            {
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                    p = (JObject)JsonConvert.DeserializeObject(product);
                }
            }
            return p;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<string> Index2()
        {
            JObject p = new JObject();
            string product="";
            //p["name"] = "aaaa";
            //p["credit"] = 5000;
            var stringContent = new StringContent(p.ToString());
            using (HttpResponseMessage response = await client.PostAsync("https://roulettegame.azurewebsites.net/game/create", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                }
            }
            return product;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<string> Index3()
        {
            string product = "";
            JObject p = new JObject();
            p["name"] = "aaaa";
            p["credit"] = 5000;
            //var stringContent = new StringContent(p.ToString());
            //var parameters = new Dictionary<string, string> { { "name", "aaa" }, { "credit", "5000"} };
            //var encodedContent = new FormUrlEncodedContent(parameters);
            var jsonInString = JsonConvert.SerializeObject(p);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonInString);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (HttpResponseMessage response = await client.PostAsync("https://roulettegame.azurewebsites.net/player/create", byteContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                }
            }
            return product;
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