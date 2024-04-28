using System.ComponentModel.DataAnnotations;

namespace SmallBookLibrary.Contracts
{
    public class BookDto
    {
       public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
