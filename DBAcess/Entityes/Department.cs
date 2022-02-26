using DBAcess.Entityes.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBAcess.Entityes
{
    public class Department : NamedEntity
    {



        public ICollection<Employee> Employees { get; set; }
        
  
        public int? HeadID { get; set; }

        public Employee Head { get; set; }

        public override string ToString() => $"{Name} (id={Id})";
    }




}
