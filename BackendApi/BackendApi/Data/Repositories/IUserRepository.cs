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
        User GetUser(LoginViewModel viewModel);
        Task<User> RegisterUser(RegisterViewModel viewModel);
    }
}
