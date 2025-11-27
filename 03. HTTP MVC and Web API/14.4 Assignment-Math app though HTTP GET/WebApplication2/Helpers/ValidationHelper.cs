namespace WebApplication2.Helpers
{
    public static class ValidationHelper
    {
        public static string[] Operations => ["add", "subtract", "multiply", "divide", "mod"];

        public static void SetStatusCode(this HttpResponse httpResponse, int statusCode)
        {
            if (httpResponse.StatusCode != statusCode)
                httpResponse.StatusCode = statusCode;
        }

        public static string GetErrorMessage(string propName) => $"Invalid input for '{propName}'.\n";
    }
}
