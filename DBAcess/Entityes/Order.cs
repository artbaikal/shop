using DBAcess.Entityes.Base;

namespace DBAcess.Entityes
{
    public  class Order: Entity
    {
        public int Number { get; set; }
        public string Product { get; set; }

        public Employee Employee { get; set; }
    }
}
