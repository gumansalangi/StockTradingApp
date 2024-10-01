namespace StockTradingApp.Model
{
    public class StockPrice
    {
        public string Symbol { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
    }

    public class StockPerformance
    {
        public string Symbol { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal Performance { get; set; }
    }
}
