using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdo.Business.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public override string Message => "Current user is not authorized for the operation";

        public override string Source => "1";
    }
}
