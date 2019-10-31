using AutoMapper;
using Serilog;
using StockBase.ResponseObjects;
using System;

namespace StockBase.Services
{
    public class RSIService
    {
        private IEXRequestService _iexRequestService { get; }
        private ILogger Logger { get; }
        private IMapper Mapper { get; }

        public RSIService(IEXRequestService iexService, ILogger logger, IMapper mapper)
        {
            _iexRequestService = iexService;
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Uses 250 data points to calculate the RSI for a stock
        /// </summary>
        /// <param name="forTicker"></param>
        public RSIResponse CalculateRSI(string forTicker) {
            var histData = _iexRequestService.RequestHistoricalData(forTicker, 1);

            double totalGain = 0.0;
            double totalLoss = 0.0;
            int dataPoints = histData.Count;

            foreach (var data in histData)
            {
                if (data.Change > 0)
                {
                    // Stock rose
                    totalGain += data.Change;
                } else
                {
                    totalLoss += data.Change;
                }
            }

            return null;
        }
    }
}
