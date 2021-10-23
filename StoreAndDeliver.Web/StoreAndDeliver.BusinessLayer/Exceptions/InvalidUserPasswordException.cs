using System;

namespace StoreAndDeliver.BusinessLayer.Exceptions
{
    public class InvalidUserPasswordException : Exception
    {
        private const String MESSAGE = "Invalid user password.";

        public InvalidUserPasswordException()
            : base(MESSAGE) { }

        public InvalidUserPasswordException(String message)
            : base(message) { }
    }
}
