using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookByIdCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public SpesificBookViewModel Model {get; set; }
        public int BookId { get; set;}

        public GetBookByIdCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public SpesificBookViewModel Handle(){
            var book = _dbContext.Books.SingleOrDefault(X => X.Id==BookId);
            if(book == null)
                throw new InvalidOperationException("The book you searched could not found.");
            var bookModel = _mapper.Map<SpesificBookViewModel>(book); //new SpesificBookViewModel();
            
            // bookModel.Title = book.Title;
            // bookModel.Genre = ((GenreEnum)book.GenreId).ToString();
            // bookModel.PageCount = book.PageCount;
            // bookModel.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
    
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