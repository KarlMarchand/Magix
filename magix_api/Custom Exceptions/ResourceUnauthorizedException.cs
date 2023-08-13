namespace magix_api.Custom_Exceptions
{
    public class ResourceUnauthorizedException : Exception
    {
        public ResourceUnauthorizedException()
        {
        }

        public ResourceUnauthorizedException(string message)
            : base(message)
        {
        }

        public ResourceUnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
