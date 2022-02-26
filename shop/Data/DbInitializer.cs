using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DBAcess.Context;
using DBAcess.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace shop.Data
{
    class DbInitializer
    {
        private readonly ShopDb _db;
        private readonly ILogger<DbInitializer> _Logger;

        public DbInitializer(ShopDb db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация БД...");

            //_Logger.LogInformation("Удаление существующей БД...");
            //await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //_Logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            //_db.Database.EnsureCreated();

            _Logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            _Logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.Departments.AnyAsync()) 
                return;

            await InitializeNewValues();


            _Logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int __DepartmentsCount = 10;

        private Department[] _Departments;

        private Employee[] _Employees;

        private const int __OrdersCount = 10;

        private Order[] _Orders= new Order[__OrdersCount];
        private async Task InitializeNewValues()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация данных...");


            var rnd = new Random();



            _Employees = new Employee[__DepartmentsCount];
            for (var i = 0; i < __DepartmentsCount; i++)
                _Employees[i] = new Employee
                {
                    Name = $"Имя {i + 1}",
                    Surname = $"Фамилия {i + 1}",
                    Patronymic = $"Отчество {i + 1}",
                    Sex = rnd.Next(0, 2) == 1 ? EnumSex.Male : EnumSex.Female,
                    Birthday = Convert.ToDateTime("01.01.2000")
                };

            _Departments = new Department[__DepartmentsCount];
            for (var i = 0; i < __DepartmentsCount; i++)
            {
                //var empl = new Employee[1];
                //empl[0] = _Employees[i];
                _Departments[i] = new Department
                {
                    Name = $"Подразделение {i + 1}",
                    Head = _Employees[i],
//                    Employees = empl
                };

            }


            await _db.Departments.AddRangeAsync(_Departments);
            await _db.SaveChangesAsync();


            for (var i = 0; i < __DepartmentsCount; i++)
            {
                var empl = new Employee[1];
                empl[0] = _Employees[i];
                _Departments[i].Employees = empl;

            }
            _db.Departments.UpdateRange(_Departments);

            await _db.SaveChangesAsync();

            for (var i = 0; i < __OrdersCount; i++)
            {

                rnd = new Random();


                _Orders[i] = new Order
                {

                    Product = $"Товар {i + 1}",
                    Number = i + 1,
                    Employee = rnd.NextItem(_Employees)
                };

            }


            await _db.Orders.AddRangeAsync(_Orders);
            await _db.SaveChangesAsync();





            _Logger.LogInformation("Инициализация данных выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        
    }
}
