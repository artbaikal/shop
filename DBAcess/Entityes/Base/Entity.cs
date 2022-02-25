using DBInterfaces;
using System.ComponentModel.DataAnnotations;

namespace DBAcess.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
