using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication2.Services.Contracts
{
    public interface IFinnhubService
    {
        Task<Dictionary<string, object>?> GetCompanyProfileAsync(string stockSymbol);
        Task<Dictionary<string, object>?> GetStockPriceQuoteAsync(string stockSymbol);
    }
}
