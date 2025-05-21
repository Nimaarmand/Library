using Application.Features.Definitions.Books;
using Application.Features.Implementations.Books;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



namespace Application
{
    public static class ApplicationConfigurator
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)

        { 

           
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IBookService, BookService>();






        }

    }

}
