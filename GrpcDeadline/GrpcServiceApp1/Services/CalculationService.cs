using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer1;
using static GrpcServer1.Division;
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

        public override async Task<CalculationResult> Add(CalculationRequest request, ServerCallContext context)
        {
            int result = request.Number1 + request.Number2;
            await Task.Delay(5000);
            var calculationResult = new CalculationResult { Result = result };

            return calculationResult;
        }

        public override async Task<CalculationResult> Subtract(CalculationRequest request, ServerCallContext context)
        {
            GrpcChannel? subtractionChannel = GrpcChannel.ForAddress("http://localhost:5502");
            var subtractiontionClient = new SubtractionClient(subtractionChannel);
            var subtractiontionRequest = new SubtractionRequest() { Number1 = request.Number1, Number2 = request.Number2 };

            DateTime deadLine = context.Deadline;
            SubtractionResult? result = subtractiontionClient.Subtract(subtractiontionRequest, deadline:deadLine);
            var calculationResult = new CalculationResult { Result = result.Result };

            await subtractionChannel.ShutdownAsync();

            return calculationResult;
        }

        public override async Task<CalculationResult> Multiply(CalculationRequest request, ServerCallContext context)
        {
            int result = request.Number1 * request.Number2;
            await Task.Delay(5000);
            var calculationResult = new CalculationResult { Result = result };

            return calculationResult;
        }

        public override async Task<CalculationResult> Divide(CalculationRequest request, ServerCallContext context)
        {
            GrpcChannel? divisionChannel = GrpcChannel.ForAddress("http://localhost:5502");
            var divisionClient = new DivisionClient(divisionChannel);
            var divisionRequest = new DivisionRequest() { Number1 = request.Number1, Number2 = request.Number2 };

            DateTime deadLine = context.Deadline;
            DivisionResult? result = divisionClient.Divide(divisionRequest, deadline: deadLine);
            var calculationResult = new CalculationResult { Result = result.Result };

            await divisionChannel.ShutdownAsync();

            return calculationResult;
        }
    }
}