using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
                {
                    if(context.Books.Any())
                    {
                        return;
                    }

                    context.Books.AddRange(
                        new Book{
                            //Id = 1, DatabaseGenerated - yani autoincremented sistem kurdugumuz icin id'yi vermemiz gereksiz hale geldi(Books sinifinda)
                            Title = "Kim Korkar API'den?",
                            GenreId = 1, //Software
                            PageCount = 353,
                            PublishDate = new DateTime(2023,02,01)
                        },
                        new Book{
                            //Id = 2,
                            Title = "Introduce to ChatGPT?", 
                            GenreId = 1, //Software
                            PageCount = 20,
                            PublishDate = new DateTime(2023,03,22)
                        },
                        new Book{
                            //Id = 3,
                            Title = "Mastery",
                            GenreId = 2, //Personal Growth
                            PageCount = 416,
                            PublishDate = new DateTime(2012,12,13)
                        }
                    );

                    context.SaveChanges();
                }      
        }
    }
}