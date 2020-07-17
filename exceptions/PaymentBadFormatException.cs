using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT_XML
{
    class PaymentBadFormatException : Exception
    {
        public PaymentBadFormatException(int i) : base("Wrong number of fields for Payment " + i) { }
    }
}
