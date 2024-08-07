using Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using RestClient.Repositories;

namespace RestClient
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRestClient(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IPlaceholderUserRepository, PlaceholderUserRepository>();
            services.AddSingleton<RestClient>();
            return services;
        }
    }
}
