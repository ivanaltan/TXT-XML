using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT_XML
{
    class BadInputDataException : Exception
    {
        public BadInputDataException(string message) : base(message) { }
    }
}
