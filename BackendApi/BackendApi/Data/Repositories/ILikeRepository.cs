using BackendApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public interface ILikeRepository
    {
        int GetLikesCount(int articleId);
        bool LikeIsPutted(int articleId, int userId);
        Task<bool> PutLike(int articleId, int userId);
        Task<bool> CancelLike(int articleId, int userId);
    }
}
