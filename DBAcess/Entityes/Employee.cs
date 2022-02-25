using DBAcess.Entityes.Base;
using System;
using System.Collections.Generic;

namespace DBAcess.Entityes
{
    public class Employee: NamedEntity
    {
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }

        public EnumSex Sex { get; set; }

        public int? DepartmentID { get; set; }
        public Department Department { get; set; }


        public Department HeadedDepartment { get; set; }

  



        public override string ToString() => $"{Surname} {Name} {Patronymic}";
    }

}
