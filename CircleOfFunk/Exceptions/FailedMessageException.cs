using System;

namespace CircleOfFunk.Exceptions
{
    public class FailedMessageException : Exception
    {
        public FailedMessageException()
        {
        }

        public FailedMessageException(string message) : base(message)
        {
        }

        public FailedMessageException(string message, Exception inner) :base(message, inner)
        {
        }
    }
}