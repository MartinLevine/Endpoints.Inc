using System;
using Endpoints.Framework.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Endpoints.Framework.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtGenerator(this IServiceCollection services, Action<GenerateOptions> genOptions)
        {
            services.AddTransient(builder =>
            {
                GenerateOptions op = new GenerateOptions();
                genOptions(op);
                return new JwtGenerator(op);
            });
            return services;
        }
    }
}
