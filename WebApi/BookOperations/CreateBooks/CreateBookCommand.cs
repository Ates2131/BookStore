using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBooks
{
    public class CreateBookCommand
    {
        public CreateBookViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var newBook = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (newBook is not null)
                throw new InvalidOperationException("This book is already registered."); //Modelden fırlattigini posttan tutman lazim
        
            newBook = _mapper.Map<Book>(Model);//new Book();
            // newBook.Title = Model.Title;
            // newBook.GenreId = Model.GenreId;
            // newBook.PageCount = Model.PageCount;
            // newBook.PublishDate = Model.PublishDate;

            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();
        }
        public class CreateBookViewModel
        {
        public string Title { get; set; }

        public int GenreId {get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; }
        }
    }
}