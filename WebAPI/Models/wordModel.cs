using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class wordModel
    {
        public int id { get; set; }
        public int users_id { get; set; }
        public int technos_id { get; set; }
        public string word { get; set; }
        public string translation { get; set; }
    }
}