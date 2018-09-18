using Creekdream.Domain.Entities;
using Creekdream.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Books
{
    /// <summary>
    /// 书信息
    /// </summary>
    public class Book : Entity<Guid>, IHasCreationTime
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
    }
}
