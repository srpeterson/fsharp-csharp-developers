namespace CoinChallenge.Api.CSharp.Domain
{
    public class StandardResponse
    {
        public string Status { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}