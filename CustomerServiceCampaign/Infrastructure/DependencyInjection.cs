﻿using Domain.Interfaces;
using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Repositories;
using Infrastructure.ExternalServices;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CustomerServiceCampaignDB")));

            //Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserExternalService, UserExternalService>();

            return services;
        }
    }
}
