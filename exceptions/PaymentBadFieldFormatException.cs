using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT_XML
{
    class PaymentBadFieldFormatException : Exception
    {
        public PaymentBadFieldFormatException(string field, int i) : base("Wrong format for " + field + " for Payment " + i) { }
    }
}
