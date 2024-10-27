using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Connexa.Application.Commons.Interfaces;
using Connexa.Domain.Commons;
using Connexa.Infra.Persistence;
using Connexa.Infra.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Connexa.Infra
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ConnexaContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")!).UseSnakeCaseNamingConvention());

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ConnexaContext>());


            return services;
        }
    }
}