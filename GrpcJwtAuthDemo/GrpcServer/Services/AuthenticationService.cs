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

        public override Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var authenticationResponse = JwtAuthenticationManager.Authenticate(request);
            return Task.FromResult(new AuthenticationResponse
            {
                AccessToken = "Hello",
                ExpiresIn = 123
            });
        }
    }
}