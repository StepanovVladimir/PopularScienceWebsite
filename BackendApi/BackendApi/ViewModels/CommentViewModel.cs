using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public string Text { get; set; }

        public int? ArticleId { get; set; }
    }
}
