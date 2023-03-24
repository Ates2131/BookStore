using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id);
            List<BooksViewModel> viewModel = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                viewModel.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.ToString("dd/MM/yyyy")
                });
            }
            return viewModel;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }

        public string Genre {get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }  
    }
}