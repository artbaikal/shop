using System;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Host;

        public static IHost Host => __Host ??= App.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();


    


        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) {
            
            services.AddServices1().AddViewModels();
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
