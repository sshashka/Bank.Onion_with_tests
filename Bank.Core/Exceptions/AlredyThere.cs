namespace Bank.Core.Exceptions
{
    public class AlredyThere : System.Exception
    {
        public AlredyThere(string message = "") : base(message) { }
    }
}