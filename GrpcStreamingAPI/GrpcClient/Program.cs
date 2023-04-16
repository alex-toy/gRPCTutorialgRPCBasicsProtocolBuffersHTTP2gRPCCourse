using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using static GrpcServer.StreamDemo;

async Task ServerStreamingDemo()
{
    GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");

    var client = new StreamDemoClient(channel);
    var request = new Test() { TestMessage = "hello friends" };
    AsyncServerStreamingCall<Test>? response = client.ServerStreamingDemo(request);
    while (await response.ResponseStream.MoveNext(CancellationToken.None))
    {
        string? value = response.ResponseStream.Current.TestMessage;
        Console.WriteLine(value);
    }

    Console.WriteLine("Server streaming completed");
    await channel.ShutdownAsync();
}

async Task ClientStreamingDemo()
{
    GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");
    var random = new Random();

    var client = new StreamDemoClient(channel);
    var stream = client.ClientStreamingDemo();
    for (int i = 0; i <= 10; i++)
    {
        var product = new Product() { Name = $"product {i}", Price = i };
        await stream.RequestStream.WriteAsync(product);
        Console.WriteLine($"Sent request for product {product.Name}");

        var randomNumber = random.Next(1, 3);
        await Task.Delay(randomNumber * 1000);
    }

    await stream.RequestStream.CompleteAsync();
    await channel.ShutdownAsync();
    Order order = await stream.ResponseAsync;
    Console.WriteLine($"Client streaming completed. Total price : {order.TotalPrice}");
}

async Task BidirectionalStreamingDemo()
{
    GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");
    var random = new Random();

    var client = new StreamDemoClient(channel);
    var stream = client.BidirectionalStreamingDemo();

    var requestTask = Task.Run(async () =>
    {
        for (int i = 0; i < 10; i++)
        {
            var randomNumber = random.Next(1, 3);
            await Task.Delay(randomNumber * 1000);

            var product = new Product() { Name = $"product {i}", Price = i };
            await stream.RequestStream.WriteAsync(product);
            Console.WriteLine($"Sent request for product {product.Name}");
        }
        await stream.RequestStream.CompleteAsync();
    });

    var responseTask = Task.Run(async () =>
    {
        while (await stream.ResponseStream.MoveNext(CancellationToken.None))
        {
            string productName = stream.ResponseStream.Current.Name;
            bool isAvailable = stream.ResponseStream.Current.IsAvailable;
            Console.WriteLine(isAvailable ? $"Product {productName} is available" : $"Product {productName} is not available");
        }
        Console.WriteLine($"Bidirectional response streaming completed.");
    });

    await Task.WhenAll(requestTask, responseTask);
    await channel.ShutdownAsync();
}


//await ClientStreamingDemo();
//await ServerStreamingDemo();
await BidirectionalStreamingDemo();