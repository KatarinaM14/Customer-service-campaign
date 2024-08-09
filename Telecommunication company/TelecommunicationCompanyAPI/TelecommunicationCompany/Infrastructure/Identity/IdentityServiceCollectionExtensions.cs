using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    //public class IdentityServiceCollectionExtensions
    //{
    //    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        services.AddDbContext<ApplicationDbContext>(options =>
    //            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    //        services.AddIdentity<IdentityUser, IdentityRole>()
    //            .AddEntityFrameworkStores<ApplicationDbContext>()
    //            .AddDefaultTokenProviders();

    //        return services;
    //    }
    //}
}
