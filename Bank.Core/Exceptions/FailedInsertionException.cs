using System;

namespace Bank.Core.Exceptions
{
    public class FailedInsertionException : System.Exception
    {
        public FailedInsertionException(string message = "") : base(message) { }
    }
}