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
            //modelBuilder.Entity<Department>()
            //.HasOptional(x => x.Manager)
            //.WithMany()
            //.HasForeignKey(x => x.ManagerID);


            //modelBuilder.Entity<Department>().HasOne(p=>p.Head).WithMany(t=>t.)
            modelBuilder.Entity<Employee>().HasOne(p => p.Department).WithMany(t => t.Employees)
                .HasForeignKey(x => x.DepartmentID).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Department>().HasOne<Employee>(p=>p.Head).WithOne(t=>t.HeadedDepartment).
                HasForeignKey<Department>(x=>x.HeadID).OnDelete(DeleteBehavior.SetNull);


            //       modelBuilder.Entity<User>()
            //.HasOne(p => p.Company)
            //.WithMany(t => t.Users)
            //.OnDelete(DeleteBehavior.Cascade);

            //        modelBuilder.Entity<EntityA>()
            //.HasRequired(a => a.EntityB)
            //.WithMany()
            //.HasForeignKey(a => a.EntityBID);

            //        modelBuilder.Entity<EntityB>()
            //            .HasOptional(b => b.PreferredEntityA)
            //            .WithMany()
            //            .HasForeignKey(b => b.PreferredEntityAID);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
