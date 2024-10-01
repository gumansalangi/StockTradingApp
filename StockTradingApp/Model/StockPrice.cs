namespace StockTradingApp.Model
{
    public class StockPrice
    {
        public string Symbol { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
    }
}
