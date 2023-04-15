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
                Test test = new Test { TestMessage = message };
                await responseStream.WriteAsync(test);
            }
        }
    }
}