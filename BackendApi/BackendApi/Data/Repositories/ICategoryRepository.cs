using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategory(int id);
        Task<bool> CreateCategory(CategoryViewModel viewModel);
        Task<bool> UpdateCategory(int id, CategoryViewModel viewModel);
        Task<bool> DeleteCategory(int id);
    }
}
