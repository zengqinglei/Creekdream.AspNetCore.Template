using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.Books.Dto;
using Zql.Application.Service;
using Zql.Application.Service.Dto;
using Zql.AutoMapper;
using Zql.Domain.Repositories;
using Zql.Orm.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.ProjectName.Books
{
    /// <inheritdoc />
    public class BookService : ApplicationService, IBookService
    {
        private readonly IRepository<Book, int> _bookRepository;

        /// <inheritdoc />
        public BookService(IRepository<Book, int> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <inheritdoc />
        public async Task<GetBookOutput> Get(int id)
        {
            var book = await _bookRepository.GetAsync(id);
            return book.MapTo<GetBookOutput>();
        }

        /// <inheritdoc />
        public async Task<PagedResultOutput<GetBookOutput>> GetPaged(GetPagedBookInput input)
        {
            #region dapper
            //Expression<Func<Book, bool>> predicate = m => m.Name.Contains(input.Name);
            //var totalCount = await _bookRepository.CountAsync(predicate);
            //// dapper 分页算法会导致误差
            //var books = await _bookRepository.GetPaged(
            //    predicate,
            //    input.SkipCount / input.MaxResultCount,
            //    input.MaxResultCount,
            //    input.Sorting);
            #endregion

            #region entityframework
            var query = _bookRepository.GetQueryIncluding();
            if (!string.IsNullOrEmpty(input.Name))
            {
                query = query.Where(m => m.Name.Contains(input.Name));
            }
            var totalCount = await query.CountAsync();
            var books = await query.OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();
            #endregion

            return new PagedResultOutput<GetBookOutput>()
            {
                TotalCount = totalCount,
                Items = books.MapTo<List<GetBookOutput>>()
            };
        }

        /// <inheritdoc />
        public async Task<GetBookOutput> Add(AddBookInput input)
        {
            var book = input.MapTo<Book>();
            book = await _bookRepository.InsertAsync(book);
            return book.MapTo<GetBookOutput>();
        }

        /// <inheritdoc />
        public async Task<GetBookOutput> Update(int id, UpdateBookInput input)
        {
            var book = await _bookRepository.GetAsync(id);
            input.MapTo(book);
            book = await _bookRepository.UpdateAsync(book);
            return book.MapTo<GetBookOutput>();
        }

        /// <inheritdoc />
        public async Task Delete(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
