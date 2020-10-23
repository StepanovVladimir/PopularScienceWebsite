using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendApi.Data.Repositories;
using BackendApi.Models;
using BackendApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentRepository _repository;

        public CommentsController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("article/{articleId}")]
        public IActionResult ArticleComments(int articleId)
        {
            var comments = _repository.GetArticleComments(articleId);
            return Ok(comments.ConvertAll(c => new { c.Id, c.Text, c.ArticleId, c.UserId, c.CreatedAt, UserName = c.User.Name }));
        }

        [HttpGet("user/{idUser}")]
        [Authorize]
        public IActionResult UserComments(int userId)
        {
            var comments = _repository.GetUserComments(userId);
            return Ok(comments.ConvertAll(c => new { c.Id, c.Text, c.ArticleId, c.UserId, c.CreatedAt, ArticleTitle = c.Article.Title }));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public IActionResult Comments()
        {
            var comments = _repository.GetComments();
            return Ok(comments.ConvertAll(c => new { c.Id, c.Text, c.ArticleId, c.UserId, c.CreatedAt, ArticleTitle = c.Article.Title, UserName = c.User.Name }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentViewModel request)
        {
            if (!request.ArticleId.HasValue)
            {
                return BadRequest();
            }

            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Comment comment;
            try
            {
                comment = await _repository.CreateComment(request, userId);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (comment != null)
            {
                return Ok(new { comment.Id, comment.Text, comment.ArticleId, comment.UserId, comment.CreatedAt, UserName = comment.User.Name });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, CommentViewModel request)
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Comment comment;
            try
            {
                comment = await _repository.UpdateComment(id, request, userId);
            }
            catch (Exception)
            {
                return NotFound();
            }

            if (comment != null)
            {
                return Ok(new { comment.Id, comment.Text, comment.ArticleId, comment.UserId, comment.CreatedAt, UserName = comment.User.Name });
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            bool successfullDelete;
            if (User.IsInRole("Moderator"))
            {
                try
                {
                    successfullDelete = await _repository.DeleteComment(id);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            else
            {
                var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
                try
                {
                    successfullDelete = await _repository.DeleteComment(id, userId);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }

            if (successfullDelete)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}