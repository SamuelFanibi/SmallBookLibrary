using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace SmallBookLibrary.Interfaces
{
    public interface IEntity<Tkey>
    {
        [Key]
        public Tkey Id { get; set; }

        //public bool IsDeleted { get; set; }
        //public DateTime? DeletedOn { get; set; }
    }
}
