using AutoMapper;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using static WebApi.BookOperations.UpdateBooks.UpdateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookViewModel, Book>(); //Model objesi Book a maplenebilsin dedik, (<source, target>)
            CreateMap<UpdatedBookViewModel, Book>();
            CreateMap<Book, SpesificBookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)(src.GenreId)).ToString())); //burda source-target yer degistirdi cunku get yaparken book objesinden viewModele geciyoruz
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)(src.GenreId)).ToString()));
            
            
        }
    }
}