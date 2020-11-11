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
    public class ArticlesController : ControllerBase
    {
        private string ImagesUrl { get; } = "https://localhost:5001/api/images/";

        private IArticleRepository _repository;

        public ArticlesController(IArticleRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var articles = _repository.GetArticles();
            return Ok(articles.ConvertAll(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                Image = ImagesUrl + a.Image,
                CreatedAt = a.CreatedAt.ToShortDateString()
            }));
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult CategoryArticles(int categoryId)
        {
            var articles = _repository.GetCategoryArticles(categoryId);
            return Ok(articles.ConvertAll(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                Image = ImagesUrl + a.Image,
                CreatedAt = a.CreatedAt.ToShortDateString()
            }));
        }

        [HttpGet("favourite")]
        [Authorize]
        public IActionResult Favourite()
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var articles = _repository.GetFavouriteArticles(userId);
            return Ok(articles.ConvertAll(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                Image = ImagesUrl + a.Image,
                CreatedAt = a.CreatedAt.ToShortDateString()
            }));
        }

        [HttpGet("seen")]
        [Authorize]
        public IActionResult Seen()
        {
            var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var articles = _repository.GetSeenArticles(userId);
            return Ok(articles.ConvertAll(a => new
            {
                a.Id,
                a.Title,
                a.Description,
                Image = ImagesUrl + a.Image,
                CreatedAt = a.CreatedAt.ToShortDateString()
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(int id)
        {
            Article article;
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
                article = await _repository.GetArticle(id, userId);
            }
            else
            {
                article = _repository.GetArticle(id);
            }

            if (article == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                article.Id,
                article.Title,
                article.Description,
                article.Content,
                Image = ImagesUrl + article.Image,
                CreatedAt = article.CreatedAt.ToShortDateString()
            });
        }

        [HttpGet("{id}/categories")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowToEdit(int id)
        {
            var article = _repository.GetArticleWithCategoryIds(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                article.Id,
                article.Title,
                article.Description,
                article.Content,
                CategoryIds = article.ArticleCategories.ConvertAll(ac => ac.CategoryId)
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm]ArticleViewModel request)
        {
            if (request.Image == null)
            {
                return BadRequest();
            }

            var articleId = await _repository.CreateArticle(request);
            if (articleId != 0)
            {
                return Ok(new { Id = articleId });
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromForm]ArticleViewModel request)
        {
            try
            {
                if (await _repository.UpdateArticle(id, request))
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _repository.DeleteArticle(id))
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