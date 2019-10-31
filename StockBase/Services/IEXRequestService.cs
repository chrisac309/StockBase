using AutoMapper;
using RestSharp;
using Serilog;
using StockBase.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockBase.Services
{
    /// <summary>
    /// Will be deprecated in the next iteration -- IEX does not provide granular information that
    /// I would like to receive.  Use AlphaVantage with ALA8JHOFP1ZG4QWN key
    /// </summary>
    public class IEXRequestService
    {
        private ILogger Logger { get; }
        private IMapper Mapper { get; }
        private RestClient Client { get; }

        public IEXRequestService(ILogger logger, IMapper mapper)
        {
            Client = new RestClient("https://api.iextrading.com/1.0");

            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<HistoricalDataResponse> RequestHistoricalData(string forSymbol, int withTimespan)
        {
            var histData = new List<HistoricalDataResponse>();
            var IEXChart_API_PATH = "/stock/{0}/chart";

            IEXChart_API_PATH = string.Format(IEXChart_API_PATH, forSymbol);

            var request = new RestRequest(IEXChart_API_PATH, Method.GET);

            var response = Client.ExecuteTaskAsync<List<HistoricalDataResponse>>(request)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessful)
            {
                histData = response.Data;
                Logger.Information("Retrieved {0} historical data points for {1}", histData.Count, forSymbol);
            }
            else
            {
                Logger.Error("Failed to retrieve historical data for stock {0}", forSymbol);
            }

            return histData;
        }
    }
}
