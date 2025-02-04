using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDo.Application.Results.Authenticate
{
    public static class AuthenticateErrors
    {
        public static readonly Error InvalidUserId = new Error(
            "AuthenticateErrors.InvalidUserId", "Invalid UserId. UserId is required.");
    }
}
