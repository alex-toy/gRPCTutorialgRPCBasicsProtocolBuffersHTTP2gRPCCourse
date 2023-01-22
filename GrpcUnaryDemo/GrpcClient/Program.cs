using Grpc.Net.Client;
using GrpcService.Protos;

namespace GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            GrpcChannel? channel = GrpcChannel.ForAddress("http://localhost:5000");
            Sample.SampleClient? client = new Sample.SampleClient(channel);
            SampleResponse? response = await client.GetFullNameAsync(new SampleRequest() { FirstName = "alex", LastName = "el bg" });
            Console.WriteLine(response);

            Console.ReadLine();
        }
    }
}
