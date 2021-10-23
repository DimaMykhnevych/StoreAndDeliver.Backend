using System;

namespace StoreAndDeliver.BusinessLayer.Constants
{
    public static class ErrorMessagesConstants
    {
        public const String NOT_ALL_PASS_FIELDS_FILLED = "Not all password fields are filled.";
        public const String PASSWORDS_DO_NOT_MATCH = "New password and confirm password must be equal.";

        public const String USERNAME_ALREADY_TAKEN = "usernameAlreadyTaken";
        public const String INVALID_PASSWORD = "invalidPassword";
    }
}
