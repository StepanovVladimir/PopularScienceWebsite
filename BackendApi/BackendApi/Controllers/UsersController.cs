using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _repository.GetUsers();
            return Ok(users.ConvertAll(u => new
            {
                u.Id,
                u.Name,
                IsModerator = u.UserRoles.Any(ur => ur.RoleId == 2)
            }));
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GiveRights(int id)
        {
            try
            {
                if (await _repository.GiveRights(id))
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DepriveRights(int id)
        {
            try
            {
                if (await _repository.DepriveRights(id))
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }

            return StatusCode(500);
        }
    }
}