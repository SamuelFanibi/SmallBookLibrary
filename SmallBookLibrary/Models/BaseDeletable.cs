using SmallBookLibrary.Interfaces;

namespace SmallBookLibrary.Models
{
    public abstract class BaseDeletable<Tkey> : BaseModel<Tkey>, IDeletable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
