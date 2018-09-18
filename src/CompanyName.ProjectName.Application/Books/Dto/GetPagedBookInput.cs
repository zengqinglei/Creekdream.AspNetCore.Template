using Creekdream.Application.Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Books.Dto
{
    /// <summary>
    /// 分页查询书信息条件
    /// </summary>
    public class GetPagedBookInput : PagedAndSortedResultInput
    {
        /// <summary>
        /// 书名(模糊匹配)
        /// </summary>
        [Display(Name = "书名")]
        [MaxLength(Book.MaxNameLength)]
        public string Name { get; set; }
    }
}
