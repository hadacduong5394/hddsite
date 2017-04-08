using HD.Domain.Models;
using HD.Infrastructure.Repository;

namespace HD.Repository.Interface
{
    public interface IPostLikeRepository : IRepositoryBase<PostLike, string>
    {
        bool CheckDupeLike(int productId, string ipAddress);

        bool CheckDupeDisLike(int productId, string ipAddress);
    }
}