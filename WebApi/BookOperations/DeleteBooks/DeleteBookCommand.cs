using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBooks
{
    public class DeleteBookCommand
    {
        public BookStoreDbContext _dbContext;
        public int BookId { get; set;}

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var deletedBook = _dbContext.Books.SingleOrDefault(X => X.Id==BookId);
            if(deletedBook == null)
                throw new InvalidOperationException("Book you wanted to delete could not found.");
            
            _dbContext.Remove(deletedBook);
            _dbContext.SaveChanges();
        }

    }
}