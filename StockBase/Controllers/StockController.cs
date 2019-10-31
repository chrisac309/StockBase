using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Web.Http;
using StockBase.ResponseObjects;
using StockBase.Services;
using Newtonsoft.Json;

namespace StockBase
{
    /// <summary>
    /// Retrieve basic stock information
    /// </summary>
    public class StockController : ApiController
    {
        private IEXRequestService IexService { get; }
        private RSIService RsiService { get; }

        private ILogger Logger { get; }
        private IMapper Mapper { get; }

        public StockController(ILogger logger, IMapper mapper, 
            IEXRequestService iexRequestService, RSIService rsiService)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            IexService = iexRequestService ?? throw new ArgumentNullException(nameof(iexRequestService));
            RsiService = rsiService ?? throw new ArgumentNullException(nameof(rsiService));
        }

        // GET <controller>
        public IEnumerable<string> Get()
        {
            Logger.Information("URL: {HttpRequestUrl}");
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets historical data for the stock using a ticker
        /// </summary>
        /// <param name="symbol">The ticker for the desired stock</param>
        /// <returns>Historical data for the stock</returns>
        [HttpGet]
        [Route("data/{symbol}")]
        public string GetHistoricalData(string symbol)
        {
            Logger.Information("Historical data request on: {0}", symbol);
            return JsonConvert.SerializeObject(IexService.RequestHistoricalData(symbol, 1));
        }

        /// <summary>
        /// Gets the Relative Strength Index (RSI) for the stock
        /// </summary>
        /// <param name="symbol">The ticker for the desired stock</param>
        /// <returns>The RSI (from 0 to 100) for the stock</returns>
        [HttpGet]
        [Route("rsi/{symbol}")]
        public string GetRSI(string symbol)
        {
            Logger.Information("Historical data request on: {0}", symbol);
            return JsonConvert.SerializeObject(RsiService.CalculateRSI(symbol));
        }

        /// <summary>
        /// Throws an exception for funsies
        /// </summary>
        [HttpGet]
        [Route("ThrowException")]
        public void ThrowException()
        {
            throw new Exception("Example exception");
        }
    }
}