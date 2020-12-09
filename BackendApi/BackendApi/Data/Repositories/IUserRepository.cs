using BackendApi.Models;
using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(LoginViewModel viewModel);
        Task<User> RegisterUser(RegisterViewModel viewModel);
        Task<bool> ChangePassword(int id, ChangePasswordViewModel viewModel);
        Task<bool> GiveRights(int id);
        Task<bool> DepriveRights(int id);
    }
}
