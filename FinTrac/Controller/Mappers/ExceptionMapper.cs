using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public class ExceptionMapper : Exception
    {
        public ExceptionMapper(string message) : base(message) { }
    }
}
