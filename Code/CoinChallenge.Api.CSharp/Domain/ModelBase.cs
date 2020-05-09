namespace CoinChallenge.Api.CSharp.Domain
{
    public abstract class ModelBase
    {
        public OperationResult OperationResult { get; internal set; }

        public string Message { get; internal set; } = string.Empty;
    }
}