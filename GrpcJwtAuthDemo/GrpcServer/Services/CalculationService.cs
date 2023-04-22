using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace GrpcServer.Services
{
    [Authorize]
    public class CalculationService : Calculation.CalculationBase
    {
        private readonly ILogger<CalculationService> _logger;

        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger;
        }

        public override Task<CalculationResult> Add(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 + request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        public override Task<CalculationResult> Subtract(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 - request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        public override Task<CalculationResult> Multiply(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 * request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        public override Task<CalculationResult> Divide(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 / request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }
    }
}