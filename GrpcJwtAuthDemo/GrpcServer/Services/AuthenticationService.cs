using Grpc.Core;
using GrpcServer.Managers;

namespace GrpcServer.Services
{
    public class AuthenticationService : Authentication.AuthenticationBase
    {
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ILogger<AuthenticationService> logger)
        {
            _logger = logger;
        }

        public override async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var authenticationResponse = JwtAuthenticationManager.Authenticate(request);
            if (authenticationResponse == null)
            {
                string message = "Invalid user credentials";
                throw new RpcException(new Status(StatusCode.Unauthenticated, message));
            }

            return authenticationResponse;
        }
    }
}