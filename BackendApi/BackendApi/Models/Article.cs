using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("article")]
    public class Article
    {
        [Column("id_article")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Required]
        [Column("image")]
        public string Image { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>(); 

        public List<ArticleCategory> ArticleCategories { get; set; } = new List<ArticleCategory>();

        public List<Like> Likes { get; set; } = new List<Like>();
        public List<View> Views { get; set; } = new List<View>();
    }
}
