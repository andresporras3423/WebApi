﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class english_projectEntities : DbContext
    {
        public english_projectEntities()
            : base("name=english_projectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<technos> technos { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<words> words { get; set; }
    
        public virtual ObjectResult<get_next_question_Result> get_next_question(Nullable<int> users_id, Nullable<int> technos_id)
        {
            var users_idParameter = users_id.HasValue ?
                new ObjectParameter("users_id", users_id) :
                new ObjectParameter("users_id", typeof(int));
    
            var technos_idParameter = technos_id.HasValue ?
                new ObjectParameter("technos_id", technos_id) :
                new ObjectParameter("technos_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<get_next_question_Result>("get_next_question", users_idParameter, technos_idParameter);
        }
    }
}
