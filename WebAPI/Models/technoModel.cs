using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class technoModel
    {
        public int id { get; set; }
        public int users_id { get; set; }
        public string techno_name { get; set; }
        public bool techno_status { get; set; }
    }
}