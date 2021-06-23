using System;

namespace MyEdo.Business.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public override string Message => "Current user is not authorized for the operation";

        public override string Source => "1";
    }
}
