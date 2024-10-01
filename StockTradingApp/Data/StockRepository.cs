using StockTradingApp.Model;
using StockTradingApp.Service;
using System.Reflection.Metadata.Ecma335;

namespace StockTradingApp.Data
{
    public class StockRepository : IStockRepository
    {
        private static List<StockPrice> _stockPrices;

        public StockRepository()
        {
            // Populate with 1 million rows of sample data if not already populated
            if (_stockPrices == null || !_stockPrices.Any())
            {
                _stockPrices = SampleDataGenerator.GenerateStockPrices(1000000);
            }
        }

        public List<StockPrice> GetAllStocks()
        {
            return _stockPrices.Take(100).ToList(); // Paginate results (for performance)
        }

        public List<StockPrice> GetStockBySymbol(string symbol)
        {
            return _stockPrices.Where(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void AddStock(StockPrice stock)
        {
            _stockPrices.Add(stock);
        }

        public void UpdateStock(string symbol, StockPrice updatedStock)
        {
            var stock = _stockPrices.FirstOrDefault(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
            if (stock != null)
            {
                stock.Price = updatedStock.Price;
                stock.Timestamp = updatedStock.Timestamp;
            }
        }

        public void DeleteStock(string symbol)
        {
            _stockPrices.RemoveAll(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
        }

        public List<StockPerformance> GetTopPerformers(int n)
        {
            List<StockPerformance> lstStocPerformance = new List<StockPerformance>();
            lstStocPerformance = _stockPrices
                .GroupBy(s => s.Symbol)
                .Select(g => new StockPerformance
                {
                    Symbol = g.Key,
                    MinPrice = g.Min(s => s.Price),
                    MaxPrice= g.Max(s => s.Price),
                    Performance = g.Max(s => s.Price) - g.Min(s => s.Price)
                })
                .OrderByDescending(p => p.Performance)
                .Take(n)
                .ToList();
            return lstStocPerformance;
        }
    }
}
