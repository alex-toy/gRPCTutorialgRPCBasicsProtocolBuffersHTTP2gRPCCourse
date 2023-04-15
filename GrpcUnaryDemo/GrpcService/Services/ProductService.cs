using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class ProductService : Product.ProductBase
    {
        public override Task<ProductSaveResponse> SaveProduct(ProductModel request, ServerCallContext context)
        {
            // insert into the database

            Console.WriteLine($"{request.ProductName} - {request.ProductCode} - {request.ProductPrice}");

            ProductSaveResponse product = new ProductSaveResponse()
            {
                StatusCode = 1,
                IsSuccessfull = true
            };

            return Task.FromResult(product);
        }

        public override Task<ProductList> GetProducts(Empty request, ServerCallContext context)
        {
            // Get from database
            var stockDate = DateTime.SpecifyKind(new DateTime(2022, 2, 2), DateTimeKind.Utc);
            var products = new ProductList();
            products.Products.Add(new ProductModel()
            {
                ProductName = "Product 001",
                ProductPrice = 133,
                ProductCode = "P01",
                StockDate = Timestamp.FromDateTime(stockDate)
            });
            products.Products.Add(new ProductModel()
            {
                ProductName = "Product 002",
                ProductPrice = 876,
                ProductCode = "P02",
                StockDate = Timestamp.FromDateTime(stockDate)
            });

            return Task.FromResult(products);
        }
    }
}
