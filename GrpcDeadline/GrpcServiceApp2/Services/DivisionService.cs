using Grpc.Core;
using GrpcServer;
using GrpcServer2;

namespace GrpcServiceApp2.Services
{
    public class DivisionService : Division.DivisionBase
    {
        private readonly ILogger<SubtractionService> _logger;
        public DivisionService(ILogger<SubtractionService> logger)
        {
            _logger = logger;
        }

        public override async Task<DivisionResult> Divide(DivisionRequest request, ServerCallContext context)
        {
            int result = request.Number1 / request.Number2;
            await Task.Delay(5000);
            var divisionResult = new DivisionResult { Result = result };

            return divisionResult;
        }
    }
}