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

    await channel.ShutdownAsync();
    Console.ReadLine();
}

Run();

void Add(CalculationClient calculationClient, int a, int b)
{
    var calculationRequest = new CalculationRequest() { Number1 = a, Number2 = b };
    CalculationResult? calculationResult = calculationClient.Add(calculationRequest);
    Console.WriteLine($"Add : {calculationResult.Result}");
}

void Sub(CalculationClient calculationClient, int a, int b)
{
    var inputNumbers = new CalculationRequest() { Number1 = a, Number2 = b };
    CalculationResult? calculationResult = calculationClient.Subtract(inputNumbers);
    Console.WriteLine($"Subtract : {calculationResult.Result}");
}
