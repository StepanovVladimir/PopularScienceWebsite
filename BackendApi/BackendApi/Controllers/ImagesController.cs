using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Data.FileManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImagesFileManager _fileManager;

        public ImagesController(IImagesFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet("{image}")]
        public IActionResult Index(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.GetImageStream(image), $"image/{mime}");
        }
    }
}