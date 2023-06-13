using CodeSmashWithAngular.Configurations;
using CodeSmashWithAngular.DatabaseContext;
using CodeSmashWithAngular.Interfaces;
using CodeSmashWithAngular.Repository;
using Microsoft.EntityFrameworkCore;

namespace CodeSmashWithAngular.Helpers
{
    public static class DependencyServices
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserUIRepository, UserUIRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

           // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(AutoMapperProfiles));

            return services;
        }
    }
}
