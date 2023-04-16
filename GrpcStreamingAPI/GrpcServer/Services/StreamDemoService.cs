using Grpc.Core;

namespace GrpcServer.Services
{
    public class StreamDemoService : StreamDemo.StreamDemoBase
    {
        private readonly ILogger<StreamDemoService> _logger;
        private Random _random;

        public StreamDemoService(ILogger<StreamDemoService> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        public override async Task ServerStreamingDemo(Test request, IServerStreamWriter<Test> responseStream, ServerCallContext context)
        {
            Console.WriteLine("start sending responses :");

            for (int i = 0; i <= 20; i++)
            {
                var randomNumber = _random.Next(1, 3);
                await Task.Delay(randomNumber * 1000);

                string message = $"Message {i} - {request.TestMessage}";
                Console.WriteLine(message);
                Test test = new Test { TestMessage = message };
                await responseStream.WriteAsync(test);
            }
        }

        public override async Task<Order> ClientStreamingDemo(IAsyncStreamReader<Product> productRequests, ServerCallContext context)
        {
            double totalPrice = 0;
            while (await productRequests.MoveNext())
            {
                string productName = productRequests.Current.Name;
                double productPrice= productRequests.Current.Price;
                totalPrice += productPrice;
                Console.WriteLine($"Product Name : {productName} - Product price {productPrice}");
            }

            Console.WriteLine($"Client streaming completed. Total price : {totalPrice}");

            var order = new Order() { TotalPrice = totalPrice };
            return order;

        }

        public override async Task BidirectionalStreamingDemo(IAsyncStreamReader<Product> productRequests, IServerStreamWriter<ProductStatus> responseStream, ServerCallContext context)
        {
            var tasks = new List<Task>();

            while (await productRequests.MoveNext())
            {
                string productName = productRequests.Current.Name;
                double productPrice = productRequests.Current.Price;
                Console.WriteLine($"Product Name : {productName} - Product price {productPrice}");

                var task = Task.Run(async () =>
                {
                    var randomNumber = _random.Next(1, 3);
                    await Task.Delay(randomNumber * 1000);

                    bool isAvailable = productPrice % 2 == 0;
                    Console.WriteLine($"product {productName} is " + (isAvailable ? "available" : "not available") );
                    var status = new ProductStatus() { Name = productName, IsAvailable = isAvailable };
                    await responseStream.WriteAsync(status);
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            Console.WriteLine("Bidirectional streaming completed");
        }
    }
}