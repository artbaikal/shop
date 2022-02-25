using Microsoft.Extensions.DependencyInjection;

namespace shop.ViewModels
{
    static class ViewModelRegistrator1
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
           .AddScoped<MainWindowViewModel>()
        ;
    }
}
