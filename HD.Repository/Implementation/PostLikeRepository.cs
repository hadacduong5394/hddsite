using System;
using HD.Domain.Models;
using HD.Infrastructure.DBFactory;
using HD.Infrastructure.Repository;
using HD.Repository.Interface;

namespace HD.Repository.Implementation
{
    public class PostLikeRepository : RepositoryBase<PostLike, string>, IPostLikeRepository
    {
        public PostLikeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public bool CheckDupeDisLike(int productId, string ipAddress)
        {
            return CheckContains(n => n.ProductId == productId && n.IPAddress.Equals(ipAddress) && n.IsLike == false);
        }

        public bool CheckDupeLike(int productId, string ipAddress)
        {
            return CheckContains(n => n.ProductId == productId && n.IPAddress.Equals(ipAddress) && n.IsLike == true);
        }
    }
}