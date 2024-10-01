using StockTradingApp.Model;

namespace StockTradingApp.Data
{
    public interface IStockRepository
    {
        List<StockPrice> GetAllStocks();
        List<StockPrice> GetStockBySymbol(string symbol);
        void AddStock(StockPrice stock);
        void UpdateStock(string symbol, StockPrice updatedStock);
        void DeleteStock(string symbol);
        List<StockPerformance> GetTopPerformers(int n);

    }
}
