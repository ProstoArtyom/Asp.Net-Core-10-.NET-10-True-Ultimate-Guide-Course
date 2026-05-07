using WebApplication1.Entities;

namespace WebApplication1.ServiceContracts.DTOs
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        public string StockSymbol { get; set; } = string.Empty;

        public string StockName {  get; set; } = string.Empty;

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not SellOrderResponse) return false;

            SellOrderResponse other = (SellOrderResponse)obj;
            return SellOrderID == other.SellOrderID
                && StockSymbol == other.StockSymbol
                && StockName == other.StockName
                && DateAndTimeOfOrder == other.DateAndTimeOfOrder
                && Quantity == other.Quantity
                && Price == other.Price
                && TradeAmount == other.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price
            };
        }
    }
}
