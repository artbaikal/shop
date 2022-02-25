using DBAcess.Entityes.Base;
using System.ComponentModel.DataAnnotations;

namespace DBAcess.Entityes
{
    public  class Order: Entity
    {
        public int Number { get; set; }
        public string Product { get; set; }
        
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
