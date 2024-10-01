using StockTradingApp.Model;

namespace StockTradingApp.Data
{
    public class SampleDataGenerator
    {
        private static Random _random = new Random();

        public static List<StockPrice> GenerateStockPrices(int numberOfRows)
        {
            var stockSymbols = new[] { "AAPL", "MSFT", "GOOG", "AMZN", "TSLA", "META" }; // Example symbols
            var stockPrices = new List<StockPrice>();

            for (int i = 0; i < numberOfRows; i++)
            {
                var symbol = stockSymbols[_random.Next(stockSymbols.Length)];
                var timestamp = DateTime.Now.AddMinutes(-_random.Next(100000)); // Random timestamp
                var price = Math.Round((decimal)(_random.NextDouble() * 1000 + 1), 2); // Random price between 1 and 1000

                stockPrices.Add(new StockPrice
                {
                    Symbol = symbol,
                    Timestamp = timestamp,
                    Price = price
                });
            }

            return stockPrices;
        }
    }
}
