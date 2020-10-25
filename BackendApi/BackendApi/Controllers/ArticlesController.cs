using System;
using System.Collections.Generic;
using System.Linq;
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
        private IArticleRepository _repository;

        public ArticlesController(IArticleRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return Ok(_repository.GetArticles());
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult CategoryArticles(int categoryId)
        {
            return Ok(_repository.GetCategoryArticles(categoryId));
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            var article = _repository.GetArticle(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
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
                return Ok(articleId);
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