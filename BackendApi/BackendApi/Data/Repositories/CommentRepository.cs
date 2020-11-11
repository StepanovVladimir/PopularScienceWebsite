using BackendApi.Models;
using BackendApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetCommentsCount(int articleId)
        {
            return _context.Comments.Where(c => c.ArticleId == articleId).Count();
        }

        public List<Comment> GetArticleComments(int articleId)
        {
            return _context.Comments.Where(c => c.ArticleId == articleId).OrderByDescending(c => c.CreatedAt).Include(c => c.User).ToList();
        }

        public List<Comment> GetUserComments(int userId)
        {
            return _context.Comments.Where(c => c.UserId == userId).OrderByDescending(c => c.CreatedAt).Include(c => c.Article).ToList();
        }

        public List<Comment> GetComments()
        {
            return _context.Comments.OrderByDescending(c => c.CreatedAt).Include(c => c.Article).Include(c => c.User).ToList();
        }

        public async Task<bool> CreateComment(CommentViewModel viewModel, int userId)
        {
            if (!_context.Articles.Any(a => a.Id == viewModel.ArticleId))
            {
                throw new Exception();
            }

            var comment = new Comment
            {
                Text = viewModel.Text,
                ArticleId = viewModel.ArticleId.Value,
                UserId = userId,
                CreatedAt = DateTime.Now,
            };

            _context.Add(comment);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateComment(int id, CommentViewModel viewModel, int userId)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                throw new Exception();
            }

            if (comment.UserId != userId)
            {
                throw new Exception();
            }

            comment.Text = viewModel.Text;

            _context.Update(comment);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                throw new Exception();
            }

            _context.Remove(comment);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteComment(int id, int userId)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                throw new Exception();
            }

            if (comment.UserId != userId)
            {
                throw new Exception();
            }

            _context.Remove(comment);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
