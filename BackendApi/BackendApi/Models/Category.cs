using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("category")]
    public class Category
    {
        [Column("id_category")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        public List<ArticleCategory> ArticleCategories { get; set; } = new List<ArticleCategory>();
    }
}
