using Api_IngaTasks.Application.Entities;
using Api_IngaTasks.Infraestructure.Configuracao.EntitiesMapping;
using Api_IngaTasks.Infraestructure.Configuracao.EntititesMapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Configuracao
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public new DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeTracker> TimeTrackers { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; } 
        
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        { 
            base.OnModelCreating (modelBuilder); //preciso disso para o Identity funcionar.
            modelBuilder.ApplyConfiguration(new ApplicationUserMapping());
            modelBuilder.ApplyConfiguration(new CollaboratorMapping());
            modelBuilder.ApplyConfiguration(new ProjectMapping());
            modelBuilder.ApplyConfiguration(new TimeTrackerMapping());
            modelBuilder.ApplyConfiguration(new TaskMapping());
        }
    }
}
