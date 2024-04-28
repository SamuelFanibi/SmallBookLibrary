namespace SmallBookLibrary.DataAccess.Seed
{
    public static class BookDataSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                 new Book { Id = Guid.NewGuid(), Title = "New book", Author = "Samuel Fanibi", Year = 2024, CreatedBy = "Samuel Fanibi", CreatedOn = DateTime.UtcNow, ModifiedBy = "Samuel Fanibi", ModifiedOn = DateTime.UtcNow, IsDeleted=false },
                  new Book { Id = Guid.NewGuid(), Title = ".Net introduction", Author = "Samuel Fanibi", Year = 2024, CreatedBy = "Samuel Fanibi", CreatedOn = DateTime.UtcNow, ModifiedBy = "Samuel Fanibi", ModifiedOn = DateTime.UtcNow, IsDeleted = false },
                   new Book { Id = Guid.NewGuid(), Title = "Java", Author = "Samuel Fanibi", Year = 2024, CreatedBy = "Samuel Fanibi", CreatedOn = DateTime.UtcNow, ModifiedBy = "Samuel Fanibi", ModifiedOn = DateTime.UtcNow, IsDeleted = false },
                    new Book { Id = Guid.NewGuid(), Title = "React", Author = "Samuel Fanibi", Year = 2024, CreatedBy = "Samuel Fanibi", CreatedOn = DateTime.UtcNow, ModifiedBy = "Samuel Fanibi", ModifiedOn = DateTime.UtcNow, IsDeleted = false },
                     new Book { Id = Guid.NewGuid(), Title = "DevOps Course", Author = "Samuel Fanibi", Year = 2024, CreatedBy = "Samuel Fanibi", CreatedOn = DateTime.UtcNow, ModifiedBy = "Samuel Fanibi", ModifiedOn = DateTime.UtcNow, IsDeleted = false }
                ) ;
        }
    }
}
