﻿using Application.Services;
using CustomerServiceCampaign.Services;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPurchaseReportService, PurchaseReportService>();

            return services;
        }
    }
}
