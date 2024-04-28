using System.ComponentModel.DataAnnotations;

namespace SmallBookLibrary.Models
{
    public class Book: BaseDeletable<Guid>
    {
        public Book() 
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.ModifiedOn = DateTime.UtcNow;
        }

        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [StringLength(100)]
        public string Author {  get; set; } = string.Empty;
        public int Year {  get; set; }
    }
}
