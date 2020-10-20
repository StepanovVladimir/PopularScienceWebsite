using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Auth;
using BackendApi.Data;
using BackendApi.Data.Repositories;
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
        private IUserRepository _userRepository;
        private JwtGenerator _jwtGenerator;

        public AuthController(IUserRepository userRepository, JwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel request)
        {
            var user = _userRepository.GetUser(request);
            if (user != null)
            {
                var token = _jwtGenerator.GenerateJwt(user);

                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel request)
        {
            var user = await _userRepository.RegisterUser(request);
            if (user != null)
            {
                var token = _jwtGenerator.GenerateJwt(user);

                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }
    }
}