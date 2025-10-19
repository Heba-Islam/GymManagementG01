using Microsoft.EntityFrameworkCore;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Contexts
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=GymManagement;Trusted_Connection=true;TrustServerCertificate=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region DbSets

        public DbSet<Member> Members { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MemberSessions> MemberSessions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Plan> Plans { get; set; }

        public DbSet<HealthRecord> HealthRecords { get; set; }

        #endregion

    }
}
