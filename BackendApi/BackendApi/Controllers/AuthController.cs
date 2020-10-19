using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Auth;
using BackendApi.Data;
using BackendApi.Models;
using BackendApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AppDbContext _context;
        private PasswordHasher<User> _passwordHasher;
        private GeneratorJwt _generatorJwt;

        public AuthController(AppDbContext context, PasswordHasher<User> passwordHasher, GeneratorJwt generatorJwt)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _generatorJwt = generatorJwt;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel request)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Name == request.Name);

            if (user != null)
            {
                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    var token = _generatorJwt.Generate(user);

                    return Ok(new { access_token = token });
                }
            }

            return Unauthorized();
        }
    }
}