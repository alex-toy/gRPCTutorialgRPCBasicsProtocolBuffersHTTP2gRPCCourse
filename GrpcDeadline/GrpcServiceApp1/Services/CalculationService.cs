using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer1;
using static GrpcServer1.Subtraction;

namespace GrpcServiceApp1.Services
{
    public class CalculationService : Calculation.CalculationBase
    {
        private readonly ILogger<CalculationService> _logger;

        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger;
        }

        public override Task<CalculationResult> Add(CalculationRequest request, ServerCallContext context)
        {
            int result = request.Number1 + request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        public override Task<CalculationResult> Subtract(CalculationRequest request, ServerCallContext context)
        {
            GrpcChannel? subtractionChannel = GrpcChannel.ForAddress("http://localhost:5502");
            var subtractiontionClient = new SubtractionClient(subtractionChannel);
            var subtractiontionRequest = new SubtractionRequest() { Number1 = request.Number1, Number2 = request.Number2 };
            SubtractionResult? result = subtractiontionClient.Subtract(subtractiontionRequest);
            var calculationResult = new CalculationResult { Result = result.Result };

            return Task.FromResult(calculationResult);
        }

        //public override Task<CalculationResult> Multiply(CalculationRequest request, ServerCallContext context)
        //{
        //    int result = request.Number1 * request.Number2;
        //    var calculationResult = new CalculationResult { Result = result };

        //    return Task.FromResult(calculationResult);
        //}

        //public override Task<CalculationResult> Divide(CalculationRequest request, ServerCallContext context)
        //{
        //    int result = request.Number1 / request.Number2;
        //    var calculationResult = new CalculationResult { Result = result };

        //    return Task.FromResult(calculationResult);
        //}
    }
}