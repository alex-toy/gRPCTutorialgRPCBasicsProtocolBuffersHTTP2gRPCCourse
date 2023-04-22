using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;
using static GrpcClient.Calculation;

Console.WriteLine("Hello, World!");


async void Run()
{
    var n1 = 100;
    var n2 = 25;

    Console.WriteLine($"n1 : {n1}");
    Console.WriteLine($"n2 : {n2}");

    GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5001");
    var calculationClient = new CalculationClient(channel);

    Add(calculationClient, n1, n2);
    Sub(calculationClient, n1, n2);
    Multiply(calculationClient, n1, n2);
    Divide(calculationClient, n1, n2);

    await channel.ShutdownAsync();
    Console.WriteLine("Channel closed");
    Console.ReadLine();
}

Run();

void Add(CalculationClient calculationClient, int a, int b)
{
    var calculationRequest = new CalculationRequest() { Number1 = a, Number2 = b };
    DateTime delay = DateTime.UtcNow.AddSeconds(3);
    CalculationResult? calculationResult = new CalculationResult();
    try
    {
        calculationResult = calculationClient.Add(calculationRequest, deadline:delay);
        Console.WriteLine($"Add : {calculationResult.Result}");
    }
    catch (RpcException ex)
    {
        if (ex.StatusCode == StatusCode.DeadlineExceeded) Console.WriteLine($"Add : {ex.Message}");
    }
}

void Sub(CalculationClient calculationClient, int a, int b)
{
    var inputNumbers = new CalculationRequest() { Number1 = a, Number2 = b };
    DateTime deadline = DateTime.UtcNow.AddSeconds(6);

    try
    {
        CalculationResult? calculationResult = calculationClient.Subtract(inputNumbers, deadline: deadline);
        Console.WriteLine($"Subtract : {calculationResult.Result}");
    }
    catch (RpcException ex)
    {
        if (ex.StatusCode == StatusCode.DeadlineExceeded) Console.WriteLine($"Subtract : {ex.Message}");
    }
}

void Multiply(CalculationClient calculationClient, int a, int b)
{
    var inputNumbers = new CalculationRequest() { Number1 = a, Number2 = b };
    DateTime deadline = DateTime.UtcNow.AddSeconds(6);

    try
    {
        CalculationResult? calculationResult = calculationClient.Multiply(inputNumbers, deadline: deadline);
        Console.WriteLine($"Multiply : {calculationResult.Result}");
    }
    catch (RpcException ex)
    {
        if (ex.StatusCode == StatusCode.DeadlineExceeded) Console.WriteLine($"multiply : {ex.Message}");
    }
}

void Divide(CalculationClient calculationClient, int a, int b)
{
    var inputNumbers = new CalculationRequest() { Number1 = a, Number2 = b };
    DateTime deadline = DateTime.UtcNow.AddSeconds(4);

    try
    {
        CalculationResult? calculationResult = calculationClient.Divide(inputNumbers, deadline: deadline);
        Console.WriteLine($"Divide : {calculationResult.Result}");
    }
    catch (RpcException ex)
    {
        if (ex.StatusCode == StatusCode.DeadlineExceeded) Console.WriteLine($"division : {ex.Message}");
    }
}
