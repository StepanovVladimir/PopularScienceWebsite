using BackendApi.Data.FileManagers;
using BackendApi.Models;
using BackendApi.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public List<Article> GetCategoryArticles(int categoryId)
        {
            return _context.Articles.Include(a => a.ArticleCategories).Where(a => a.ArticleCategories.Any(ac => ac.CategoryId == categoryId)).ToList();
        }

        public Article GetArticle(int id)
        {
            return _context.Articles.Find(id);
        }

        public Article GetArticleWithCategoryIds(int id)
        {
            return _context.Articles.Include(a => a.ArticleCategories).FirstOrDefault(a => a.Id == id);
        }

        public async Task<int> CreateArticle(ArticleViewModel viewModel)
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
                return 0;
            }

            _context.Add(article);
            if (await _context.SaveChangesAsync() > 0)
            {
                foreach (int categoryId in viewModel.CategoryIds)
                {
                    _context.Add(new ArticleCategory { ArticleId = article.Id, CategoryId = categoryId });
                }

                await _context.SaveChangesAsync();

                return article.Id;
            }

            return 0;
        }

        public async Task<bool> UpdateArticle(int id, ArticleViewModel viewModel)
        {
            var article = GetArticleWithCategoryIds(id);

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
                    return false;
                }
                _fileManager.DeleteImage(previousImage);
            }

            _context.Update(article);

            foreach (ArticleCategory articleCategory in article.ArticleCategories)
            {
                if (!viewModel.CategoryIds.Any(ci => ci == articleCategory.CategoryId))
                {
                    _context.Remove(articleCategory);
                }
            }

            foreach (int categoryId in viewModel.CategoryIds)
            {
                _context.Add(new ArticleCategory { ArticleId = article.Id, CategoryId = categoryId });
            }

            return await _context.SaveChangesAsync() > 0;
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
