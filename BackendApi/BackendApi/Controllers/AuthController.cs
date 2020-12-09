using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendApi.Auth;
using BackendApi.Data;
using BackendApi.Data.Repositories;
using BackendApi.Models;
using BackendApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private IUserRepository _repository;
        private JwtGenerator _jwtGenerator;

        public AuthController(IUserRepository repository, JwtGenerator jwtGenerator)
        {
            _repository = repository;
            _jwtGenerator = jwtGenerator;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel request)
        {
            var user = _repository.GetUser(request);
            if (user != null)
            {
                var token = _jwtGenerator.GenerateJwt(user);

                return Ok(new { AccessToken = token });
            }

            return Unauthorized();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            var user = await _repository.RegisterUser(request);
            if (user != null)
            {
                var token = _jwtGenerator.GenerateJwt(user);

                return Ok(new { AccessToken = token });
            }

            return Unauthorized();
        }

        [Route("password/change")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel request)
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                if (await _repository.ChangePassword(userId, request))
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return StatusCode(500);
        }
    }
}