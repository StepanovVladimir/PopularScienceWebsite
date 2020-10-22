using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
