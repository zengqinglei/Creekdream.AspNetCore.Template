using System;

namespace CompanyName.ProjectName.Books.Dto
{
    /// <summary>
    /// 书输出信息
    /// </summary>
    public class GetBookOutput
    {
        /// <summary>
        /// 书唯一Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; set; }
    }
}
