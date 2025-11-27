namespace WebApplication1.Helpers
{
    public static class ValidationHelper
    {
        public static void SetStatusCode(this HttpResponse httpResponse, int statusCode)
        {
            if (httpResponse.StatusCode != statusCode)
                httpResponse.StatusCode = statusCode;
        }
    }
}
