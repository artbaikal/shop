using DBAcess.Entityes.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBAcess.Entityes
{
    public class Department : NamedEntity
    {

        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; }
        
        //[NotMapped]
        public int? HeadID { get; set; }
        [ForeignKey("HeadID")]
        public Employee Head { get; set; }


        //public List<Employee> Students { get; set; } = new List<Employee>();

    }


    //public class EntityB
    //{
    //    public int ID { get; set; }
    //    public Nullable<int> PreferredEntityAID { get; set; }

    //    [ForeignKey("PreferredEntityAID")]
    //    public virtual EntityA PreferredEntityA { get; set; }

    //    [InverseProperty("EntityB")] // <- Navigation property name in EntityA
    //    public virtual ICollection<EntityA> EntityAs { get; set; }
    //}

}
