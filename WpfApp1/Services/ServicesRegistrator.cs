using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp1.Services
{
    static class ServicesRegistrator1
    {
        public static IServiceCollection AddServices1(this IServiceCollection services) {
            return services;
        }
        
    }
}
