﻿using YouDo.Core.Validations;

namespace YouDo.Application.Results.Authenticate
{
    public static class AuthenticateErrors
    {
        public static readonly Error InvalidUserId = new Error(
            "AuthenticateErrors.InvalidUserId", "Invalid UserId. UserId is required.");

        public static readonly Error InvalidEmailOrPassword = new Error(
            "AuthenticateErrors.InvalidEmailOrPassword", "Invalid email or password");

        public static Error CustomError(string customMessage)
        {
            return new Error("AuthenticateErrors", customMessage);
        }
    }
}
