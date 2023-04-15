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

await ServerStreamingDemo();