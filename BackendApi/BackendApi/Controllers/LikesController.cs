using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendApi.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private ILikeRepository _repository;

        public LikesController(ILikeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{articleId}/count")]
        public IActionResult Count(int articleId)
        {
            return Ok(new { Count = _repository.GetLikesCount(articleId) });
        }

        [HttpGet("{articleId}/putted")]
        [Authorize]
        public IActionResult Putted(int articleId)
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(new { Putted = _repository.LikeIsPutted(articleId, userId) });
        }

        [HttpPost("{articleId}")]
        [Authorize]
        public async Task<IActionResult> Put(int articleId)
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            try
            {
                if (await _repository.PutLike(articleId, userId))
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

        [HttpDelete("{articleId}")]
        [Authorize]
        public async Task<IActionResult> Cancel(int articleId)
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (await _repository.CancelLike(articleId, userId))
            {
                return NoContent();
            }

            return StatusCode(500);
        }
    }
}