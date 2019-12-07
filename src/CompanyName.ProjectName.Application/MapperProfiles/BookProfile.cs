using AutoMapper;
using CompanyName.ProjectName.Books;
using CompanyName.ProjectName.Books.Dto;

namespace CompanyName.ProjectName.MapperProfiles
{
    /// <summary>
    /// Model mapping of book entity
    /// </summary>
    public class BookProfile : Profile
    {
        /// <inheritdoc />
        public BookProfile()
        {
            CreateMap<Book, GetBookOutput>();
            CreateMap<CreateBookInput, Book>();
            CreateMap<UpdateBookInput, Book>();
        }
    }
}
