using Entities.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts;
using Services;

namespace Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

        public static void InjectDependencies(IServiceCollection services)
        {
            services.AddScoped<ProjectContext>();

            services.AddScoped(typeof(ICustomerMessageService), typeof(CustomerMessageService));
            services.AddScoped(typeof(IProjectService),typeof(ProjectService));
            services.AddScoped(typeof(IProjectImageService), typeof(ProjectImageService));
            services.AddScoped(typeof(IBlogService), typeof(BlogService));
            services.AddScoped(typeof(IBlogImageService), typeof(BlogImageService));

            services.AddDbContext<ProjectContext>(options => options.UseSqlServer(@"Server=DESKTOP-CKPPIUF; Database=KioskWeb; Trusted_Connection = true", x => x.MigrationsAssembly("UI")));


        }
    }

}
