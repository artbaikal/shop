using DBAcess.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess.Context
{
    public class ShopDb: DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public ShopDb(DbContextOptions<ShopDb> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            //modelBuilder.Entity<Department>.OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Department>()
            .HasOptional(x => x.Manager)
            .WithMany()
            .HasForeignKey(x => x.ManagerID);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
