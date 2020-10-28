using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private IViewRepository _repository;

        public ViewsController(IViewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{articleId}")]
        public IActionResult Count(int articleId)
        {
            return Ok(new { Count = _repository.GetViewsCount(articleId) });
        }
    }
}