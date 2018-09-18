using Microsoft.AspNetCore.Mvc;
using CompanyName.ProjectName.Books;
using CompanyName.ProjectName.Books.Dto;
using System.Threading.Tasks;
using Creekdream.Application.Service.Dto;
using System;

namespace CompanyName.ProjectName.Api.Controllers
{
    /// <summary>
    /// 书信息服务
    /// </summary>
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;

        /// <inheritdoc />
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// 根据Id获取书信息 
        /// </summary>
        [HttpGet("{id}")]
        public async Task<GetBookOutput> Get(Guid id)
        {
            return await _bookService.Get(id);
        }

        /// <summary>
        /// 分页查询书信息
        /// </summary>
        [HttpGet]
        public async Task<PagedResultOutput<GetBookOutput>> GetPaged([FromQuery]GetPagedBookInput input)
        {
            return await _bookService.GetPaged(input);
        }

        /// <summary>
        /// 新增书信息
        /// </summary>
        [HttpPost]
        public async Task<GetBookOutput> Post([FromBody]AddBookInput input)
        {
            return await _bookService.Add(input);
        }

        /// <summary>
        /// 修改书信息
        /// </summary>
        [HttpPut("{id}")]
        public async Task<GetBookOutput> Put([FromQuery]Guid id, [FromBody]UpdateBookInput input)
        {
            return await _bookService.Update(id, input);
        }

        /// <summary>
        /// 删除书信息
        /// </summary>
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await _bookService.Delete(id);
        }
    }
}
