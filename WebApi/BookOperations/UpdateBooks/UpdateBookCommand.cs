using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdatedBookViewModel Model {get; set; }
        public int BookId { get; set;}
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _dbContext = context;
        }
        public void Handle()
        {
            var updatedBook = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(updatedBook == null)
                throw new InvalidOperationException("The book that you want to update could not found.");
            
            updatedBook.Title = Model.Title == default ? updatedBook.Title : Model.Title;
            updatedBook.GenreId = Model.GenreId == default ? updatedBook.GenreId : Model.GenreId;
            updatedBook.PageCount = Model.PageCount == default ? updatedBook.PageCount : Model.PageCount;
            updatedBook.PublishDate = Model.PublishDate == default ? updatedBook.PublishDate : Model.PublishDate;

            _dbContext.SaveChanges();
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