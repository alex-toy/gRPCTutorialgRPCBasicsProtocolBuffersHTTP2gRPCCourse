using Grpc.Core;
using GrpcServer;

namespace GrpcServiceApp2.Services
{
    public class SubtractionService : Subtraction.SubtractionBase
    {
        private readonly ILogger<SubtractionService> _logger;
        public SubtractionService(ILogger<SubtractionService> logger)
        {
            _logger = logger;
        }

        public override async Task<SubtractionResult> Subtract(SubtractionRequest request, ServerCallContext context)
        {
            int result = request.Number1 - request.Number2;
            await Task.Delay(5000);
            var subtractionResult = new SubtractionResult { Result = result };

            return subtractionResult;
        }
    }
}