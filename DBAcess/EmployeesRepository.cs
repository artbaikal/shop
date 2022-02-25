using System.Linq;
using DBAcess.Context;
using DBAcess.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DBAcess
{
    class EmployeesRepository : DbRepository<Employee>
    {
        public override IQueryable<Employee> Items => base.Items.Include(item => item.Department)
            //.Include(item => item.Head)
            ;

        public EmployeesRepository(ShopDb db) : base(db) { }
    }
}