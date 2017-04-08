using HD.Domain.Models;

namespace HD.Service.Interface
{
    public interface IPostLikeService : IBaseService
    {
        PostLike CreateNew(PostLike postLike);

        bool CheckDupeLike(int productId, string ipAddress);

        bool CheckDupeDisLike(int productId, string ipAddress);
    }
}