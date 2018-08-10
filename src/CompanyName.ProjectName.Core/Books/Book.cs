using System;
using System.ComponentModel.DataAnnotations;
using Zql.Domain.Entities;
using Zql.Domain.Entities.Auditing;

namespace CompanyName.ProjectName.Books
{
    /// <summary>
    /// 书信息
    /// </summary>
    public class Book : Entity, IHasCreationTime
    {
        public const int MaxNameLength = 50;

        /// <summary>
        /// 书名
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        public Book()
        {
            CreationTime = DateTime.Now;
        }
    }
}
