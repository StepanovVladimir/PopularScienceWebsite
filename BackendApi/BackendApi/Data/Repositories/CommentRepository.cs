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

        public List<Comment> GetArticleComments(int articleId)
        {
            return _context.Comments.Where(c => c.ArticleId == articleId).Include(c => c.User).ToList();
        }

        public async Task<Comment> CreateComment(CommentViewModel viewModel, int userId)
        {
            if (!_context.Articles.Any(a => a.Id == viewModel.ArticleId))
            {
                throw new Exception();
            }

            var comment = new Comment
            {
                Text = viewModel.Text,
                ArticleId = viewModel.ArticleId.Value,
                UserId = userId
            };

            _context.Add(comment);
            if (await _context.SaveChangesAsync() > 0)
            {
                comment.User = _context.Users.Find(userId);
                return comment;
            }

            return null;
        }

        public async Task<Comment> UpdateComment(int id, CommentViewModel viewModel, int userId)
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
            if (await _context.SaveChangesAsync() > 0)
            {
                comment.User = _context.Users.Find(userId);
                return comment;
            }

            return null;
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
