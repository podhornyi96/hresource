using System;
using System.Collections.Generic;
using System.Text;
using HResource.EntityFramework.Entities;
using HResource.EntityFramework.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HResource.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<Company> Comapnies { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
