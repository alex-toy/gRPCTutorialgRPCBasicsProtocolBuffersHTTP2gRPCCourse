namespace GrpcServer.Managers
{
    public static partial class JwtAuthenticationManager
    {
        enum USER_ROLE
        {
            ADMIN = 1,
            USER = 2,
            EXTERNAL = 3
        }
    }
}
