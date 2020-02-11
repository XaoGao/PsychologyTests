using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Repositories;

namespace Psychology_API.DataServices.DataServices
{
    public class BaseService : BaseRepository, IBaseService
    {
        public BaseService(DataContext context) : base(context)
        {
        }
    }
}