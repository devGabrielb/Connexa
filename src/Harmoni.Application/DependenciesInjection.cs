using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Harmoni.Application
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependenciesInjection));


        });
            return services;
        }
    }
}