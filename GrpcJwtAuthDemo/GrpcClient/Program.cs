using Grpc.Net.Client;
using GrpcClient;

async Task ServerStreamingDemo()
{
    GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");

    AuthenticationResponse response = channel.GetToken();
    Console.WriteLine($"Token : {response.AccessToken}");
    Console.WriteLine($"Expires in : {response.ExpiresIn}");

    CalculationResult resultAdd = channel.GetAddResponse(2, 3);
    Console.WriteLine($"Add : {resultAdd.Result}");

    CalculationResult resultSub = channel.GetSubtractResponse(2, 3);
    Console.WriteLine($"Subtract : {resultSub.Result}");

    CalculationResult resultMult = channel.GetMultiplyResponse(2, 3);
    Console.WriteLine($"Multiply : {resultMult.Result}");

    CalculationResult resultDiv = channel.GetDivideResponse(2, 3);
    Console.WriteLine($"Divide : {resultDiv.Result}");

    await channel.ShutdownAsync();
}

await ServerStreamingDemo();

Console.ReadLine();