using DBAcess.Entityes;
using DBInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DBAcess
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Department>, DepartmentsRepository>()
            .AddTransient<IRepository<Employee>, DbRepository<Employee>>()
            .AddTransient<IRepository<Order>, DbRepository<Order>>()

        ;
    }
}
