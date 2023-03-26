using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id);
            List<BooksViewModel> viewModel = _mapper.Map<List<BooksViewModel>>(bookList);

            // foreach (var book in bookList)
            // {
            //     viewModel.Add(new BooksViewModel()
            //     {
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PageCount = book.PageCount,
            //         PublishDate = book.PublishDate.ToString("dd/MM/yyyy")
            //     });
            // }
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