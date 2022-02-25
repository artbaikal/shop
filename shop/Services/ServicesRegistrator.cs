using Microsoft.Extensions.DependencyInjection;
using shop.Services.Interfaces;

namespace shop.Services
{
    static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {
            return services.AddTransient<IUserDialog, UserDialogService>();
        }
        
    }
}
