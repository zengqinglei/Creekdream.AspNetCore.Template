using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Books.Dto
{
    /// <summary>
    /// 修改书信息
    /// </summary>
    public class UpdateBookInput
    {
        /// <summary>
        /// 书名
        /// </summary>
        [Display(Name = "书名")]
        [Required(ErrorMessage = "请填写{0}")]
        [MaxLength(Book.MaxNameLength, ErrorMessage = "最多只能填写{1}位")]
        public string Name { get; set; }
    }
}
