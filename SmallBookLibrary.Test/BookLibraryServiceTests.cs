using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SmallBookLibrary.DataAccess;
using SmallBookLibrary.Interfaces;
using SmallBookLibrary.Services;
using System.Diagnostics;
//using Xunit;

namespace SmallBookLibrary.Test
{
    [TestFixture]
    public class BookLibraryServiceTests
    {
        private Mock<AppDbContext> _mockRepository;
        private GenericRepository<Book> _genericRepository;
        private IBookService _bookService;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "BookDb")
          .Options;
            _mockRepository = new Mock<AppDbContext>(options);
            _genericRepository = new GenericRepository<Book>(_mockRepository.Object);
            _bookService = new BookService(_mockRepository.Object);
        }

        [Test]
        public async Task CreateBookAsync_CreatesNewBook()
        {
            //Arrange 
            var newbook = new Book { Id = Guid.NewGuid(), Title = "Test", Author = "Samuel", Year = 2024, CreatedBy = "Samuel", ModifiedBy = "Samuel", IsDeleted = false };

            _mockRepository.Setup(x => x.Set<Book>().AddAsync(newbook, default)).Verifiable();
            _mockRepository.Setup(x=>x.SaveChangesAsync(default)).Verifiable();

            // Act
            var result = await _bookService.CreateBookAsync(newbook);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newbook.Id, result.Id);
            Assert.AreEqual(newbook.Title, result.Title);
            _mockRepository.Verify(x => x.Set<Book>().AddAsync(newbook, default), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
        [Test]
        public async Task UpdateBookAsync_UpdateBook()
        {
            // Arrange
            var existingBook = new Book { Id = Guid.NewGuid(), Title = "Existing Book", Author="Samuel", Year=2024, IsDeleted = false, ModifiedOn = DateTime.Now, ModifiedBy="Samuel", CreatedBy="Samuel" };
            var updatedBook = new Book { Id = existingBook.Id, Title = "Updated Book", Author = "Samuel", Year = 2024, IsDeleted = false, ModifiedOn = DateTime.Now.AddDays(1), ModifiedBy="Samuel" };

            // _mockRepository.Setup(x => x.Set<Book>().AddAsync(existingBook, default)).Verifiable();
            // _mockRepository.Setup(x => x.SaveChangesAsync(default)).Verifiable();
            var mockDbSet = MockDbSet(new List<Book> { existingBook });
            _mockRepository.Setup(x => x.Set<Book>()).Returns(mockDbSet.Object);
            _mockRepository.Setup(x=>x.SaveChangesAsync(default)).Verifiable();

            // Act
            var result = await _bookService.UpdateBookAsync(updatedBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedBook.Title, result.Title);
           // _mockRepository.Verify(x => x.Set<Book>().Update(updatedBook, default), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

       
        private Mock<DbSet<TEntity>> MockDbSet<TEntity>(List<TEntity> data) where TEntity : class
        {
            var queryable = data.AsQueryable();
            var mockDbSet = new Mock<DbSet<TEntity>>();
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            return mockDbSet;
        }

    }
}