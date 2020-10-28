using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("like")]
    public class Like
    {
        [Column("id_article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Column("id_user")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
