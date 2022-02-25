using System.ComponentModel.DataAnnotations;

namespace DBAcess.Entityes.Base
{
    public abstract class NamedEntity : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}