using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Data;
using StockTradingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private static List<StockPrice> _stockPrices;

        public StockController()
        {
            // Populate with 1 million rows of sample data
            if (_stockPrices == null || !_stockPrices.Any())
            {
                _stockPrices = SampleDataGenerator.GenerateStockPrices(1000000);
            }
        }

        // GET: api/stock
        [HttpGet]
        public IActionResult GetAllStocks()
        {
            return Ok(_stockPrices.Take(100).ToList()); // Paginate results (for performance)
        }

        // GET: api/stock/{symbol}
        [HttpGet("{symbol}")]
        public IActionResult GetStockBySymbol(string symbol)
        {
            var stock = _stockPrices.Where(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase)).ToList();
            if (stock == null || !stock.Any())
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // POST: api/stock
        [HttpPost]
        public IActionResult AddStock([FromBody] StockPrice newStock)
        {
            if (newStock == null)
            {
                return BadRequest("Invalid stock data.");
            }

            _stockPrices.Add(newStock);
            return Ok(newStock);
        }

        // PUT: api/stock/{symbol}
        [HttpPut("{symbol}")]
        public IActionResult UpdateStock(string symbol, [FromBody] StockPrice updatedStock)
        {
            var stock = _stockPrices.FirstOrDefault(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
            if (stock == null)
            {
                return NotFound();
            }

            stock.Price = updatedStock.Price;
            stock.Timestamp = updatedStock.Timestamp;

            return Ok(stock);
        }

        // DELETE: api/stock/{symbol}
        [HttpDelete("{symbol}")]
        public IActionResult DeleteStock(string symbol)
        {
            var stock = _stockPrices.RemoveAll(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
            if (stock == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        // GET: api/stock/topperformers/{n}
        [HttpGet("topperformers/{n}")]
        public IActionResult GetTopPerformers(int n)
        {
            var topPerformers = _stockPrices
                .GroupBy(s => s.Symbol)
                .Select(g => new
                {
                    Symbol = g.Key,
                    MinPrice = g.Min(s => s.Price),
                    MaxPrice = g.Max(s => s.Price),
                    Performance = g.Max(s => s.Price) - g.Min(s => s.Price)
                })
                .OrderByDescending(p => p.Performance)
                .Take(n)
                .ToList();

            return Ok(topPerformers);
        }


    }
}
