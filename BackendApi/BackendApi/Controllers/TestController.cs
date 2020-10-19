using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: { User.Identity.Name }");
        }

        [Authorize(Roles = "Admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok($"Ваши роли: { User.IsInRole("Admin") } { User.IsInRole("Moderator") }");
        }

        [Authorize(Roles = "Moderator")]
        [Route("getid")]
        public IActionResult GetId()
        {
            return Ok($"Ваш id: { User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value }");
        }
    }
}