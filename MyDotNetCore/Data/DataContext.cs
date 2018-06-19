using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDotNetCore.Models;

namespace MyDotNetCore.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
       
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
           
        }

        public DbSet<BdToken> BdTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BdToken>().ToTable("BdTokens");
            base.OnModelCreating(builder);
           
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
