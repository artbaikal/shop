using DBAcess.Entityes.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBAcess.Entityes
{
    public class Department : NamedEntity
    {
      
        public ICollection<Employee> Employees { get; set; }
        
        [NotMapped]
        public Employee Head { get; set; }

        //public List<Employee> Students { get; set; } = new List<Employee>();

    }

    
}
