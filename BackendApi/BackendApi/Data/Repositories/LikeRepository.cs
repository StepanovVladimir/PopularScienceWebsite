using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetLikesCount(int articleId)
        {
            return _context.Likes.Where(l => l.ArticleId == articleId).Count();
        }

        public bool LikeIsPutted(int articleId, int userId)
        {
            return _context.Likes.Any(l => l.ArticleId == articleId && l.UserId == userId);
        }

        public async Task<int> PutLike(int articleId, int userId)
        {
            if (!_context.Articles.Any(a => a.Id == articleId))
            {
                throw new Exception();
            }

            if (LikeIsPutted(articleId, userId))
            {
                return GetLikesCount(articleId);
            }

            _context.Add(new Like { ArticleId = articleId, UserId = userId });

            if (await _context.SaveChangesAsync() > 0)
            {
                return GetLikesCount(articleId);
            }

            return -1;
        }

        public async Task<int> CancelLike(int articleId, int userId)
        {
            var like = _context.Likes.Find(articleId, userId);

            if (like == null)
            {
                return GetLikesCount(articleId);
            }

            _context.Remove(like);

            if (await _context.SaveChangesAsync() > 0)
            {
                return GetLikesCount(articleId);
            }

            return -1;
        }
    }
}
