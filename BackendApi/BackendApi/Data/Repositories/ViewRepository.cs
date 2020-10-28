using BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public class ViewRepository : IViewRepository
    {
        private AppDbContext _context;

        public ViewRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetViewsCount(int articleId)
        {
            return _context.Views.Where(v => v.ArticleId == articleId).Count();
        }
    }
}
