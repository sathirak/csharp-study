using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace console.Controllers
{
    [ApiController]
    [Route("cryptostock")]
    public class CryptoStockController : ControllerBase
    {
        private readonly ILogger<CryptoStockController> _logger;
        private readonly Random _random = new Random();

        public CryptoStockController(ILogger<CryptoStockController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet(Name = "GetCryptoStockPrices")]
        public IActionResult GetCryptoStockPrices()
        {
            try
            {
                var cryptoStockPrices = GenerateCryptoStockPrices();
                return Ok(cryptoStockPrices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating crypto stock prices");
                return StatusCode(500, "Internal Server Error");
            }
        }

        private IEnumerable<CryptoStockPrice> GenerateCryptoStockPrices()
        {
            var cryptoSymbols = new[] { "BTC", "ETH", "XRP", "LTC", "ADA" };

            return cryptoSymbols.Select(symbol => new CryptoStockPrice
            {
                Symbol = symbol,
                Price = Math.Round(_random.NextDouble() * 10000, 2) // Generating random prices for demonstration
            })
            .ToArray();
        }
    }

    public class CryptoStockPrice
    {
        public required string Symbol { get; set; }
        public double Price { get; set; }
    }
}
