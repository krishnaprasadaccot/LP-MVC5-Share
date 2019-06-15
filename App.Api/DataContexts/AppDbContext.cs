using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Api
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        public DbSet<Application> Applications { get; set; }
        public DbSet<HouseMember> HouseMembers { get; set; }
        public DbSet<HouseMemebrRelationship> Relationships { get; set; }


    }
}
