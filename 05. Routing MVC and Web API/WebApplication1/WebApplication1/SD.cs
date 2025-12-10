namespace WebApplication1
{
    public static class SD
    {
        public static IDictionary<int, string> ContriesDict { get; }
            = new Dictionary<int, string>
        {
            { 1, "United States" },
            { 2, "Canada" },
            { 3, "United Kingdom" },
            { 4, "India" },
            { 5, "Japan" }
        };
    }
}
