﻿namespace StockBase.ResponseObjects
{
    public class HistoricalDataResponse
    {
        public string Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
        public int UnadjustedVolume { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public double Vwap { get; set; }
        public string Label { get; set; }
        public double ChangeOverTime { get; set; }
    }
}