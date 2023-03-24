using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBooks;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using WebApi.BookOperations.UpdateBooks;
using static WebApi.BookOperations.UpdateBooks.UpdateBookCommand;
using WebApi.BookOperations.DeleteBooks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        // private static List<Book> BookList = new List<Book>(){
        //     new Book{
        //         Id = 1,
        //         Title = "Kim Korkar API'den?",
        //         GenreId = 1,
        //         PageCount = 35,
        //         PublishDate = new DateTime(2023,02,01)
        //     },
        //     new Book{
        //         Id = 2,
        //         Title = "Introduce to ChatGPT?",
        //         GenreId = 1,
        //         PageCount = 20,
        //         PublishDate = new DateTime(2023,03,22)
        //     },
        //     new Book{
        //         Id = 3,
        //         Title = "Mastery",
        //         GenreId = 2,
        //         PageCount = 416,
        //         PublishDate = new DateTime(2012,12,13)
        //     }
        // };

        [HttpGet()]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            SpesificBookViewModel result;
            try
            {
                GetBookByIdCommand command = new GetBookByIdCommand(_context);
                command.BookId = id;
                result = command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
        // [HttpGet("{id}")]
        // public Book GetById([FromQuery] string id){
        //     if(BookList.Where(X => X.Id == Convert.ToInt32(id)).SingleOrDefault()!=null)
        //         return BookList.Where(X => X.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     else{
        //         throw new Exception($"There is no user registered as id {id}");
        //     }
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }                       //Handle'dan atilan exceptionu tutmak icin try catch yaptik
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedBookViewModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}