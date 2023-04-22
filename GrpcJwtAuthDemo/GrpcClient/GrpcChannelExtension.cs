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

        public async static Task<CalculationResult> GetAddResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            Metadata headers = GetHeaders();
            var inputNumbers = new InputNumbers() { Number1 = a, Number2 = b };
            CalculationResult? resultAdd = await calculationClient.AddAsync(inputNumbers, headers);
            return resultAdd;
        }

        public async static Task<CalculationResult> GetSubtractResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            Metadata headers = GetHeaders();
            CalculationResult? resultAdd = await calculationClient.SubtractAsync(calculationRequest, headers);
            return resultAdd;
        }

        public async static Task<CalculationResult> GetMultiplyResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            Metadata headers = GetHeaders();
            CalculationResult? resultAdd = await calculationClient.MultiplyAsync(calculationRequest, headers);
            return resultAdd;
        }

        public async static Task<CalculationResult> GetDivideResponse(this GrpcChannel channel, int a, int b)
        {
            var calculationClient = new CalculationClient(channel);
            var calculationRequest = new InputNumbers() { Number1 = a, Number2 = b };
            Metadata headers = GetHeaders();
            CalculationResult? resultAdd = await calculationClient.DivideAsync(calculationRequest, headers);
            return resultAdd;
        }

        private static Metadata GetHeaders()
        {
            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {_authenticationResponse.AccessToken}");
            return headers;
        }
    }
}
