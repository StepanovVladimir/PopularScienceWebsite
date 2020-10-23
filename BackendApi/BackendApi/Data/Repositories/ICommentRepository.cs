using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetArticleComments(int articleId);
        List<Comment> GetUserComments(int userId);
        List<Comment> GetComments();
        Task<Comment> CreateComment(CommentViewModel viewModel, int userId);
        Task<Comment> UpdateComment(int id, CommentViewModel viewModel, int userId);
        Task<bool> DeleteComment(int id);
        Task<bool> DeleteComment(int id, int userId);
    }
}
