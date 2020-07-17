using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TXT_XML
{
    static class InputDataValidator
    {

        public static void Validate(string messageId, string time, int numPayments, double controlSum, string InitgPtyNm) {

            bool throwFlag = false;
            string throwMessage = "";

            if (!Regex.IsMatch(messageId, @"\d{7}/\d{14}"))
            {
                throwFlag = true;
                throwMessage += "Bad Message Id Format. ";
            }
                
            if(!Regex.IsMatch(time, @"(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})"))
            {
                throwFlag = true;
                throwMessage += "Bad Time Format. ";
            }

            if (!(numPayments > 0))
            {
                throwFlag = true;
                throwMessage += "No Number Of Payments. ";
            }

            if (!Regex.IsMatch(InitgPtyNm, @"[A-Z,\s]{1,}/\d{1,}"))
{
                throwFlag = true;
                throwMessage += "Bad InitgPtyNm Format. ";
            }

            if (!throwFlag)
                return;
            else
            {
                throwMessage.Remove(throwMessage.Length - 1);
                throw new BadInputDataException(throwMessage);
            }

        }

    }
}
