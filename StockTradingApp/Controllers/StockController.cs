using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Data;
using StockTradingApp.Model;
using StockTradingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;



        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // GET: api/stock
        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var stocks = _stockRepository.GetAllStocks();
            return Ok(stocks);
        }

        // GET: api/stock/{symbol}
        [HttpGet("{symbol}")]
        public IActionResult GetStockBySymbol(string symbol)
        {
            var stock = _stockRepository.GetStockBySymbol(symbol);
            if (stock == null || stock.Count == 0)
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

            _stockRepository.AddStock(newStock);
            return Ok(newStock);
        }

        // PUT: api/stock/{symbol}
        [HttpPut("{symbol}")]
        public IActionResult UpdateStock(string symbol, [FromBody] StockPrice updatedStock)
        {
            _stockRepository.UpdateStock(symbol, updatedStock);
            return Ok(updatedStock);
        }

        // DELETE: api/stock/{symbol}
        [HttpDelete("{symbol}")]
        public IActionResult DeleteStock(string symbol)
        {
            _stockRepository.DeleteStock(symbol);
            return Ok();
        }

        // GET: api/stock/topperformers/{n}
        [HttpGet("topperformers/{n}")]
        public IActionResult GetTopPerformers(int n)
        {
            var topPerformers = _stockRepository.GetTopPerformers(n);
            return Ok(topPerformers);
        }


    }
}
