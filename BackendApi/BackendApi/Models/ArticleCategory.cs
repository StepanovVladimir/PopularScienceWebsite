using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("article_category")]
    public class ArticleCategory
    {
        [Column("id_article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Column("id_category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
