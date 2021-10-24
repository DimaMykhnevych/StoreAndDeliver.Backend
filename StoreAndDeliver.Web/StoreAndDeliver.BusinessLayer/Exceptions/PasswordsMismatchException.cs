using System;

namespace StoreAndDeliver.BusinessLayer.Exceptions
{
    public class PasswordsMismatchException : Exception
    {
        private const String MESSAGE = "Passwords don't match.";

        public PasswordsMismatchException()
            : base(MESSAGE) { }

        public PasswordsMismatchException(String message)
            : base(message) { }
    }
}
