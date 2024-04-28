using Microsoft.AspNetCore.Mvc;
using SmallBookLibrary.Contracts;

namespace SmallBookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        
        private readonly ILogger<BookController> _logger;

        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet("Books")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBookbyId")]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError("The book is empty {0}", id);
                return BadRequest("Empty book Id");
            }
            try
            {
                var book = await _bookService.GetBookAsync(Guid.Parse(id));
                return Ok(book);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);  
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            try
            {
                var book = new Book()
                {
                    Title = bookDto.Title,
                    Year = bookDto.Year,
                    Author = bookDto.Author,
                    CreatedBy= "Samuel Fanibi",
                    ModifiedBy = "Samuel Fanibi"
                }; 
                
               var bk = await _bookService.CreateBookAsync(book);
                return Ok(bk);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateBook")]
        public async Task<ActionResult<Book>> UpdateBook(BookDto bookDto)
        {
            try
            {
                var book = new Book()
                {
                    Id = bookDto.Id,
                    Title = bookDto.Title,
                    Year = bookDto.Year,
                    Author = bookDto.Author,
                    ModifiedBy = "Samuel Fanibi",
                    ModifiedOn = DateTime.UtcNow,
                };

                var bk = await _bookService.UpdateBookAsync(book);
                return Ok(bk);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
