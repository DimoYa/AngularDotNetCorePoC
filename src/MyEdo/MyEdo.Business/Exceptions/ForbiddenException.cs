using System;

namespace MyEdo.Business.Exceptions
{
    public class ForbiddenException : Exception
    {
        public override string Message => "This operation is forbidden for the current user";

        public override string Source => "1";
    }
}
