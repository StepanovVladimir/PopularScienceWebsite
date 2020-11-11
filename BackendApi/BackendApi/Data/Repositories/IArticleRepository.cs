using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public interface IArticleRepository
    {
        List<Article> GetArticles();
        List<Article> GetCategoryArticles(int categoryId);
        List<Article> GetFavouriteArticles(int userId);
        List<Article> GetSeenArticles(int userId);
        Article GetArticle(int id);
        Task<Article> GetArticle(int id, int userId);
        Article GetArticleWithCategoryIds(int id);
        Task<int> CreateArticle(ArticleViewModel viewModel);
        Task<bool> UpdateArticle(int id, ArticleViewModel viewModel);
        Task<bool> DeleteArticle(int id);
    }
}
