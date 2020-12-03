using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Find(id);
        }

        public async Task<bool> CreateCategory(CategoryViewModel viewModel)
        {
            var category = new Category { Name = viewModel.Name };

            _context.Add(category);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategory(int id, CategoryViewModel viewModel)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new Exception();
            }

            category.Name = viewModel.Name;

            _context.Update(category);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                throw new Exception();
            }

            _context.Remove(category);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
