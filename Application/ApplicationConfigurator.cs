using Application.Features.Definitions.Books;
using Application.Features.Definitions.Contexts;
using Application.Features.Implementations.Books;
using Application.MappingProfile;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace Application
{
    public static class ApplicationConfigurator
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            

            // اضافه کردن MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IBookService, BookService>();
            


           

           
        }

    }

}
