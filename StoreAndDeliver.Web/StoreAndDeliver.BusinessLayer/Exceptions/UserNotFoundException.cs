using System;

namespace StoreAndDeliver.BusinessLayer.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private const String MESSAGE = "User was not found.";

        public UserNotFoundException()
            : base(MESSAGE) { }

        public UserNotFoundException(String message)
            : base(message) { }
    }
}
