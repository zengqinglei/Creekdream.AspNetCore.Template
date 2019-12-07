using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyName.ProjectName.Books.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Creekdream.Application.Service;
using Creekdream.Domain.Repositories;
using Creekdream.Application.Service.Dto;
using Creekdream.Orm.EntityFrameworkCore;
using System;
using AutoMapper;

namespace CompanyName.ProjectName.Books
{
    /// <inheritdoc />
    public class BookService : ApplicationService, IBookService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IMapper _mapper;

        /// <inheritdoc />
        public BookService(
            IRepository<Book, Guid> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<GetBookOutput> Get(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return _mapper.Map<GetBookOutput>(book);
        }

        /// <inheritdoc />
        public async Task<PagedResultOutput<GetBookOutput>> GetPaged(GetPagedBookInput input)
        {
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

            return new PagedResultOutput<GetBookOutput>()
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<GetBookOutput>>(books)
            };
        }

        /// <inheritdoc />
        public async Task<GetBookOutput> Create(CreateBookInput input)
        {
            var book = _mapper.Map<Book>(input);
            book = await _bookRepository.InsertAsync(book);
            return _mapper.Map<GetBookOutput>(book);
        }

        /// <inheritdoc />
        public async Task<GetBookOutput> Update(Guid id, UpdateBookInput input)
        {
            var book = await _bookRepository.GetAsync(id);
            _mapper.Map(input, book);
            book = await _bookRepository.UpdateAsync(book);
            return _mapper.Map<GetBookOutput>(book);
        }

        /// <inheritdoc />
        public async Task Delete(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
