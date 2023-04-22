using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace GrpcServer.Services
{
    public class CalculationService : Calculation.CalculationBase
    {
        private readonly ILogger<CalculationService> _logger;

        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "EXTERNAL, USER, ADMIN")]
        public override Task<CalculationResult> Add(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 + request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        [Authorize(Roles = "USER, ADMIN")]
        public override Task<CalculationResult> Subtract(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 - request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        [Authorize(Roles = "USER, ADMIN")]
        public override Task<CalculationResult> Multiply(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 * request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }

        [Authorize(Roles = "ADMIN")]
        public override Task<CalculationResult> Divide(InputNumbers request, ServerCallContext context)
        {
            int result = request.Number1 / request.Number2;
            var calculationResult = new CalculationResult { Result = result };

            return Task.FromResult(calculationResult);
        }
    }
}