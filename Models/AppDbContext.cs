//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Government_Helping_System.Models
{
    //public class AppDbContext : IdentityDbContext
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.seed();
        }*/

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Employee> employees { get; set; }

    }
}
