using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class ProductService : Product.ProductBase
    {
        public override Task<ProductSaveResponse> SaveProduct(ProductModel request, ServerCallContext context)
        {
            // insert into the database

            var result = Task.FromResult(new ProductSaveResponse() { 
                StatusCode = 1,
                IsSuccessfull = true
            });

            return result;
        }
    }
}
