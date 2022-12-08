using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.Entity.Entities;

namespace TrainTicket.Data.Contexts
{
    public class TrainTicketContext : IdentityDbContext<AppUser,AppRole,string>
    {
     
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TrainRoute>  TrainRoutes { get; set; }
        public DbSet<City> Cities { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=TrainTicketDB;integrated security=true;");
        }



    }
}
