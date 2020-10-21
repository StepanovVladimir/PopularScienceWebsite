using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.FileManagers
{
    public interface IImagesFileManager
    {
        FileStream GetImageStream(string image);
        Task<string> SaveImage(IFormFile image);
        void DeleteImage(string image);
    }
}
