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

        var randomNumber = random.Next(1, 3);
        await Task.Delay(randomNumber * 1000);
    }

    await stream.RequestStream.CompleteAsync();
    Order order = await stream.ResponseAsync;
    Console.WriteLine($"Client streaming completed. Total price : {order.TotalPrice}");
}


//await ServerStreamingDemo();
await ClientStreamingDemo();