namespace Bank.Core.Exceptions
{
    public class AlreadyDeletedException : System.Exception
    {
        public AlreadyDeletedException(string message = "") : base(message)
        {
        }
    }
}