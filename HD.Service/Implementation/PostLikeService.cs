using System;
using HD.Domain.Models;
using HD.Infrastructure.UnitOfWork;
using HD.Repository.Interface;
using HD.Service.Interface;

namespace HD.Service.Implementation
{
    public class PostLikeService : BaseService, IPostLikeService
    {
        private readonly IPostLikeRepository _postRepo;

        public PostLikeService(IUnitOfWork unitOfWork, IPostLikeRepository postRepo) : base(unitOfWork)
        {
            this._postRepo = postRepo;
        }

        public bool CheckDupeDisLike(int productId, string ipAddress)
        {
            return _postRepo.CheckDupeDisLike(productId, ipAddress);
        }

        public bool CheckDupeLike(int productId, string ipAddress)
        {
            return _postRepo.CheckDupeLike(productId, ipAddress);
        }

        public PostLike CreateNew(PostLike postLike)
        {
            return _postRepo.Add(postLike);
        }
    }
}