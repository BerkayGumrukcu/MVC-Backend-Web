using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Context
{
    public class ProjectContext : IdentityDbContext<User>
    {

        public ProjectContext()
        {

        }

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public virtual DbSet<CustomerMessage> CustomerMessages { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProjectImage> ProjectImages { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<BlogImage> BlogImages { get; set; }

        public virtual  DbSet<User> Users { get; set; }



    }
}
