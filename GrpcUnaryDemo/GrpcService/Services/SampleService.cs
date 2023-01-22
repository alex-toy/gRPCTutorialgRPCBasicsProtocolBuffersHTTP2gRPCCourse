using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class SampleService : Sample.SampleBase
    {
        public override Task<SampleResponse> GetFullName(SampleRequest request, ServerCallContext context)
        {
            string? result = $"{request.FirstName} {request.LastName}";
            return Task.FromResult(new SampleResponse() { FullName = result});
        }
    }
}
