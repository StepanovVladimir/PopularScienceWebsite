using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Data.Repositories
{
    public interface IViewRepository
    {
        int GetViewsCount(int articleId);
    }
}
