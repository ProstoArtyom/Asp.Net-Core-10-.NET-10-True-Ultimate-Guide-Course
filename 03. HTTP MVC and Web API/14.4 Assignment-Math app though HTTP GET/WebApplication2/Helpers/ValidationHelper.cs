namespace WebApplication2.Helpers
{
    public static class ValidationHelper
    {
        public static string[] Operations => ["add", "subtract", "multiply", "divide", "mod"];

        public static void SetStatusCode(this HttpResponse httpResponse, int statusCode)
        {
            if (httpResponse.StatusCode != 400)
                httpResponse.StatusCode = 400;
        }

        public static string GetErrorMessage(string propName) => $"Invalid input for '{propName}'.\n";
    }
}
