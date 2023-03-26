using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdatedBookViewModel Model {get; set; }
        public int BookId { get; set;}
        public UpdateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var updatedBook = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(updatedBook == null)
                throw new InvalidOperationException("The book that you want to update could not found.");
            
            // updatedBook = _mapper.Map<Book>(Model); >> If you use Automapper like that, it returns a new Patient object and the references to the enity framework graph are not kept. You have to use it like this: asagidaki kullanım aynı referanstaki objenin gösterdigi yeri degistiriyor boylece modelden mapledigimiz book databasedeki book oluyor ve onun üzerindeki degisiklikleri kaydedebiliyoruz
            _mapper.Map(Model, updatedBook);
            _dbContext.SaveChanges();
            // updatedBook.Title = Model.Title == default ? updatedBook.Title : Model.Title;
            // updatedBook.GenreId = Model.GenreId == default ? updatedBook.GenreId : Model.GenreId;
            // updatedBook.PageCount = Model.PageCount == default ? updatedBook.PageCount : Model.PageCount;
            // updatedBook.PublishDate = Model.PublishDate == default ? updatedBook.PublishDate : Model.PublishDate;

            
        }
        public class UpdatedBookViewModel
        {
        public string Title { get; set; }

        public int GenreId {get; set; }

        public int PageCount { get; set; }

        public DateTime PublishDate { get; set; }
        }
    }
}