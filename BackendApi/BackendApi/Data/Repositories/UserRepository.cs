using BackendApi.Models;
using BackendApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        private IPasswordHasher<User> _passwordHasher;

        public UserRepository(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public User GetUser(LoginViewModel viewModel)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Name == viewModel.Name);
            if (user != null)
            {
                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, viewModel.Password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<User> RegisterUser(RegisterViewModel viewModel)
        {
            if (_context.Users.Any(u => u.Name == viewModel.Name))
            {
                return null;
            }

            var user = new User { Name = viewModel.Name };
            user.PasswordHash = _passwordHasher.HashPassword(user, viewModel.Password);
            _context.Add(user);

            if (await _context.SaveChangesAsync() > 0)
            {
                return user;
            }

            return null;
        }
    }
}
