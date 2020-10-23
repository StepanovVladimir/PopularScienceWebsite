using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("comment")]
    public class Comment
    {
        [Column("id_comment")]
        public int Id { get; set; }

        [Required]
        [Column("text")]
        public string Text { get; set; }

        [Column("id_article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Column("id_user")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
