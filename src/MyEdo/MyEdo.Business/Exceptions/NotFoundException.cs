using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEdo.Business.Exceptions
{
    public class NotFoundException : MyEdoException
    {
        public override string Message => "Not found";

        public override string Source => "2";
    }
}
