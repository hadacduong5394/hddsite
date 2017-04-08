using HD.Core;
using HD.Domain.Models;
using HD.Service.Interface;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace HD.Site.Hubs
{
    public class LikeButton : Hub
    {
        public Task Like(int productId)
        {
            var proSrv = IoC.Resolve<IProductService>();
            var postSrv = IoC.Resolve<IPostLikeService>();
            var product = proSrv.GetBykey(productId);
            var ipAddress = Context.Request.GetHttpContext().Request.UserHostAddress;
            var dupeCheck = postSrv.CheckDupeLike(productId, ipAddress);
            if (!dupeCheck)
            {
                proSrv.BeginTran();
                try
                {
                    product.LikeCount = product.LikeCount + 1;
                    proSrv.Update(product);

                    postSrv.CreateNew(new PostLike {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = productId,
                        IPAddress = ipAddress,
                        IsLike = true
                    });
                    proSrv.CommitChange();
                    proSrv.CommitTran();
                }
                catch (Exception ex)
                {
                    proSrv.RollbackTran();
                    var errorSrv = IoC.Resolve<IErrorService>();
                    errorSrv.CreateNew(new Error {
                        Message = ex.Message,
                        Stacktrace = ex.StackTrace,
                        CreateDate = DateTime.Now,
                        Status = false
                    });
                    errorSrv.CommitChange();
                }                
            }

            return Clients.All.updateLikeCount(new LikeViewModel { ProductId = productId, Count = product.LikeCount });
        }

        public Task DisLike(int productId)
        {
            var proSrv = IoC.Resolve<IProductService>();
            var postSrv = IoC.Resolve<IPostLikeService>();
            var product = proSrv.GetBykey(productId);
            var ipAddress = Context.Request.GetHttpContext().Request.UserHostAddress;
            var dupeCheck = postSrv.CheckDupeDisLike(productId, ipAddress);
            if (!dupeCheck)
            {
                proSrv.BeginTran();
                try
                {
                    //product.DisLikeCount = product.DisLikeCount + 1;
                    proSrv.Update(product);

                    postSrv.CreateNew(new PostLike
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = productId,
                        IPAddress = ipAddress,
                        IsLike = false
                    });
                    proSrv.CommitChange();
                    proSrv.CommitTran();
                }
                catch (Exception ex)
                {
                    proSrv.RollbackTran();
                    var errorSrv = IoC.Resolve<IErrorService>();
                    errorSrv.CreateNew(new Error
                    {
                        Message = ex.Message,
                        Stacktrace = ex.StackTrace,
                        CreateDate = DateTime.Now,
                        Status = false
                    });
                    errorSrv.CommitChange();
                }
            }

            return Clients.All.updateDisLikeCount(new LikeViewModel { ProductId = productId/*, Count = product.DisLikeCount*/ });
        }



        public class LikeViewModel
        {
            public int ProductId { get; set; }

            public int Count { get; set; }
        }
    }
}