using System.ComponentModel.DataAnnotations;
using WebApplication1.Entities;
using WebApplication1.ServiceContracts.DTOs.ValidationAttributes;

namespace WebApplication1.ServiceContracts.DTOs
{
    public class SellOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; } = string.Empty;

        [Required]
        public string StockName { get; set; } = string.Empty;

        [MinDate("01-01-2000")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price { get; set; }

        public double TradeAmount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SellOrder ToSellOrder()
        {
            return new SellOrder
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
