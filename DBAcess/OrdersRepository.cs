using System.Linq;
using DBAcess.Context;
using DBAcess.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DBAcess
{
    class OrdersRepository : DbRepository<Order>
    {
        public override IQueryable<Order> Items => base.Items.Include(item => item.Employee)
            ;

        public OrdersRepository(ShopDb db) : base(db) { }
    }

}