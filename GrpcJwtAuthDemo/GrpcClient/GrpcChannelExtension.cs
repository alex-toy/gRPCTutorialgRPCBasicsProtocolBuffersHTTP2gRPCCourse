using Grpc.Core;
using Grpc.Net.Client;
using static GrpcClient.Authentication;
using static GrpcClient.Calculation;

namespace GrpcClient
{
    public static class GrpcChannelExtension
    {
        private static AuthenticationResponse? _authenticationResponse;

        public static AuthenticationResponse GetToken(this GrpcChannel channel)
        {
            try
            {
                SetAuthenticationResponse(channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            return _authenticationResponse;
        }

        private static void SetAuthenticationResponse(this GrpcChannel channel)
        {
            var authenticationClient = new AuthenticationClient(channel);
            var authenticationRequest = new AuthenticationRequest() { UserName = "alex", Password = "alex" };
            _authenticationResponse = authenticationClient.Authenticate(authenticationRequest);
        }

        public static CalculationResult GetAddResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            Metadata? headers = new Metadata();
            headers.Add("Authorization", $"Bearer {_authenticationResponse.AccessToken}");
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            CalculationResult? resultAdd = calculationClient.Add(calculationRequest, headers);
            return resultAdd;
        }

        public static CalculationResult GetSubtractResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            CalculationResult? resultAdd = calculationClient.Subtract(calculationRequest);
            return resultAdd;
        }

        public static CalculationResult GetMultiplyResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            CalculationResult? resultAdd = calculationClient.Multiply(calculationRequest);
            return resultAdd;
        }

        public static CalculationResult GetDivideResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            CalculationResult? resultAdd = calculationClient.Divide(calculationRequest);
            return resultAdd;
        }
    }
}
