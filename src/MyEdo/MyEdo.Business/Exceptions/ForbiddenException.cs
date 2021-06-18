using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdo.Business.Exceptions
{
    public class ForbiddenException : Exception
    {
        public override string Message => "This operation is forbidden for the current user";

        public override string Source => "1";
    }
}
