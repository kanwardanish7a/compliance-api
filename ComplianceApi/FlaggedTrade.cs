namespace ComplianceApi
{
    public class FlaggedTrade
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public string Reason { get; set; } = "";
        public bool Reviewed { get; set; }
    }
}
