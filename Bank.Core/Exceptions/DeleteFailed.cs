namespace Bank.Core.Exceptions
{
    public class DeleteFailed : System.Exception
    {
        public DeleteFailed(string message = "") : base(message) { }
    }
}