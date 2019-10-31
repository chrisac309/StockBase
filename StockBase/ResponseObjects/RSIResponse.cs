namespace StockBase.ResponseObjects
{
    public enum RSIInterpretation
    {

    }

    public class RSIResponse
    {
        public int RSI { get; set; }
        public RSIInterpretation Interpretation { get; set; }
    }
}