using BackendApi.Data.FileManagers;
using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private AppDbContext _context;
        private IImagesFileManager _fileManager;

        public ArticleRepository(AppDbContext context, IImagesFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public List<Article> GetArticles()
        {
            return _context.Articles
                .Select(a => new Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Image = a.Image,
                    CreatedAt = a.CreatedAt
                })
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
        }

        public Article GetArticle(int id)
        {
            return _context.Articles.Find(id);
        }

        public async Task<Article> CreateArticle(ArticleViewModel viewModel)
        {
            var article = new Article
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Content = viewModel.Content,
                CreatedAt = DateTime.Now
            };
            try
            {
                article.Image = await _fileManager.SaveImage(viewModel.Image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            _context.Add(article);
            if (await _context.SaveChangesAsync() > 0)
            {
                return article;
            }

            return null;
        }

        public async Task<Article> UpdateArticle(int id, ArticleViewModel viewModel)
        {
            var article = GetArticle(id);

            if (article == null)
            {
                throw new Exception();
            }

            article.Title = viewModel.Title;
            article.Description = viewModel.Description;
            article.Content = viewModel.Content;
            if (viewModel.Image != null)
            {
                string previousImage = article.Image;
                try
                {
                    article.Image = await _fileManager.SaveImage(viewModel.Image);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                _fileManager.DeleteImage(previousImage);
            }

            _context.Update(article);
            if (await _context.SaveChangesAsync() > 0)
            {
                return article;
            }

            return null;
        }

        public async Task<bool> DeleteArticle(int id)
        {
            var article = GetArticle(id);

            if (article == null)
            {
                throw new Exception();
            }

            _fileManager.DeleteImage(article.Image);
            _context.Remove(article);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
