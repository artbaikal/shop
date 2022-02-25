using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shop.Data;
using shop.Services;
using shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>


    public partial class App : Application
    {
        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window CurrentWindow => FocusedWindow ?? ActiveWindow ?? Current.MainWindow;

        private static IHost __Host;

        public static IHost Host => __Host ??= App.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();





        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {

            services
                .AddServices()
                .AddViewModels()
                .AddDatabase(host.Configuration.GetSection("Database"));
        }




        //public static IHostBuilder CreateHostBuilder(string[] args) => Host
        //   .CreateDefaultBuilder(args)
        //   .ConfigureServices(App.ConfigureServices);
        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            return Microsoft.Extensions.Hosting.Host
                  .CreateDefaultBuilder(args)
                  .ConfigureServices(App.ConfigureServices);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            using (var scope = Services.CreateScope())
                scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();

            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await host.StopAsync();
        }

    }
}
