using SmallBookLibrary.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SmallBookLibrary.Models
{
    public abstract class BaseModel<Tkey> : IAuditInfo, IEntity<Tkey>
    {
        [Key]
        public Tkey Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        [StringLength(35)]
        public string CreatedBy { get; set; } = string.Empty;
        [StringLength(35)]
        public string ModifiedBy { get; set; } = string.Empty;

    }
}
