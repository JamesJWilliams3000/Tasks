﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tasks__MVP_
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JobsEntities : DbContext
    {
        public JobsEntities()
            : base("name=JobsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Current_Music> Current_Music { get; set; }
        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<DatesJob> DatesJobs { get; set; }
        public virtual DbSet<DatesSubJob> DatesSubJobs { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        public virtual DbSet<SubJob> SubJobs { get; set; }
    }
}
