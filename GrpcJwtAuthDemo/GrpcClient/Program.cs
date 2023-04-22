using Grpc.Net.Client;
using GrpcClient;

async Task GetCalculations()
{
    GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");

    AuthenticationResponse response = channel.GetToken();
    Console.WriteLine($"Token : {response.AccessToken}");
    Console.WriteLine($"Expires in : {response.ExpiresIn}");

    CalculationResult resultAdd = await channel.GetAddResponse(2, 3);
    Console.WriteLine($"Add : {resultAdd.Result}");

    CalculationResult resultSub = await channel.GetSubtractResponse(2, 3);
    Console.WriteLine($"Subtract : {resultSub.Result}");

    CalculationResult resultMult = await channel.GetMultiplyResponse(2, 3);
    Console.WriteLine($"Multiply : {resultMult.Result}");

    try
    {
        CalculationResult resultDiv = await channel.GetDivideResponse(2, 3);
        Console.WriteLine($"Divide : {resultDiv.Result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Divide is only for admin");
        Console.WriteLine(ex);
    }

    await channel.ShutdownAsync();
}

await GetCalculations();

Console.ReadLine();