using System.Linq;
using DBAcess.Context;
using DBAcess.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DBAcess
{
    class DepartmentsRepository : DbRepository<Department>
    {
        public override IQueryable<Department> Items => base.Items.Include(item => item.Employees)
            .Include(item => item.Head)
            ;

        public DepartmentsRepository(ShopDb db) : base(db) { }
    }

}