using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcClient.Protos;
using static GrpcClient.Protos.Product;
using static GrpcClient.Protos.Sample;

namespace GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");

            var sampleClient = new SampleClient(channel);
            var sampleRequest = new SampleRequest() { FirstName = "alex", LastName = "el bg" };
            SampleResponse? sampleResponse = await sampleClient.GetFullNameAsync(sampleRequest);
            Console.WriteLine(sampleResponse);


            var productClient = new ProductClient(channel);
            var stockDate = DateTime.SpecifyKind(new DateTime(2022, 2, 2), DateTimeKind.Utc);
            var productModel = new ProductModel() { ProductName = "bike", ProductCode = "87654", ProductPrice = 543, StockDate = Timestamp.FromDateTime(stockDate) };
            ProductSaveResponse? productResponse = await productClient.SaveProductAsync(productModel);
            Console.WriteLine(productResponse);


            var productList = await productClient.GetProductsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (var product in productList.Products)
            {
                var productStockDate = product.StockDate.ToDateTime();
                Console.WriteLine($"{product.ProductName} - {product.ProductCode} - {product.ProductPrice} - {productStockDate.ToString("dd-MM-yyyy")}");
            }

            await channel.ShutdownAsync();

            Console.ReadLine();
        }

    }
}
