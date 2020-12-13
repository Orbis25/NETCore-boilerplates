using AutoMapper;
using BussinesLayer.Interfaces.Person;
using BussinesLayer.Services.Persons;
using DatabaseLayer.Contexts;
using DatabaseLayer.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiBase.Extensions
{
    public static class StartupExtension
    {
        public static void ImplementDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        }

        public static void ImplementServices(this IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();
        }

        public static void ImplementAuthMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CommonMapping));
        }
    }
}
