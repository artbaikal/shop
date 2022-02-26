using DBAcess.Entityes;
using DBInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DBAcess
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Employee>, EmployeesRepository>()
            .AddTransient<IRepository<Department>, DepartmentsRepository>()
            .AddTransient<IRepository<Order>, OrdersRepository>()

        ;
    }
}
