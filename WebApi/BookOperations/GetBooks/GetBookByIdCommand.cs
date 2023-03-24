using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookByIdCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public SpesificBookViewModel Model {get; set; }
        public int BookId { get; set;}

        public GetBookByIdCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SpesificBookViewModel Handle(){
            var book = _dbContext.Books.SingleOrDefault(X => X.Id==BookId);
            if(book == null)
                throw new InvalidOperationException("The book you searched could not found.");
            var bookModel = new SpesificBookViewModel();
            
            bookModel.Title = book.Title;
            bookModel.Genre = ((GenreEnum)book.GenreId).ToString();
            bookModel.PageCount = book.PageCount;
            bookModel.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
    
            return bookModel;
        }
        
    }
    public class SpesificBookViewModel
    {
        public string Title { get; set; }

        public string Genre {get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}