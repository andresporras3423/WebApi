using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class testModel
    {
        public int id { get; set; }
        public int users_id { get; set; }
        public System.DateTime created_datetime { get; set; }
        public int correct { get; set; }
        public int total { get; set; }
    }
}