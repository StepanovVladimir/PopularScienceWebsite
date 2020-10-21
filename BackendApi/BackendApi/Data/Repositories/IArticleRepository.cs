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
        Article GetArticle(int id);
        Task<Article> CreateArticle(ArticleViewModel viewModel);
        Task<Article> UpdateArticle(int id, ArticleViewModel viewModel);
        Task<bool> DeleteArticle(int id);
        FileStream GetImageStream(string image);
    }
}
