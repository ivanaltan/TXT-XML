using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT_XML
{
    class PaymentMissingFieldException : Exception
    {
        public PaymentMissingFieldException(string field, int i) : base("Missing " + field + " for Payment " + i) { }
    }
}
