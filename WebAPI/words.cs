//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class words
    {
        public int id { get; set; }
        public int users_id { get; set; }
        public int technos_id { get; set; }
        public string word { get; set; }
        public string translation { get; set; }
    
        public virtual technos technos { get; set; }
        public virtual users users { get; set; }
    }
}
