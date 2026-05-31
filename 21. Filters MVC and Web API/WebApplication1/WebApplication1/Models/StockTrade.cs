namespace WebApplication1.Models
{
    public class StockTrade
    {
        public string? StockSymbol { get; set; } = string.Empty;

        public string? StockName { get; set; } = string.Empty;

        public double Price { get; set; }

        public uint Quantity { get; set; }
    }
}
