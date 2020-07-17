using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace TXT_XML
{
    static class PaymentMapper
    {
        public static string IBANRegex = @"[A-Z]{2}\d{2}[A-Z\d]{1,30}";

        public static List<Payment> Map(bool TSV_CSV, string[] lines)
        {
            List<Payment> payments = new List<Payment>();

            int i = 1;
            foreach (string line in lines)
            {
                string[] fields;

                if (TSV_CSV)
                    fields = line.Split('\t');
                else
                    fields = line.Split(',');

                if (fields.Length != 21)
                    throw new PaymentBadFormatException(i);

                Payment p = new Payment();

                int currentIndex = 0;
                string currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240 && Regex.IsMatch(currentField, IBANRegex))
                    {
                        p.SourceAccount = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Source Account", i);
                }
                else throw new PaymentMissingFieldException("Source Account", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 20 && float.Parse(currentField) > 0)
                    {
                        p.PaymentAmmount = float.Parse(currentField);
                    }
                    else throw new PaymentBadFieldFormatException("Payment Amount", i);
                }
                else throw new PaymentMissingFieldException("Payment Amount", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length == 1 && ((Char.Parse(currentField)) == 'S' || (Char.Parse(currentField)) == 'U'))
                    {
                        p.Priority = Char.Parse(currentField);
                    }
                    else throw new PaymentBadFieldFormatException("Priority", i);
                }
                else throw new PaymentMissingFieldException("Priority", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 3)
                    {
                        p.PaymentMethod= currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Payment Method", i);
                }
                else throw new PaymentMissingFieldException("Payment Method", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 7)
                    {
                        p.PaymentConfirmation = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Payment Confirmation", i);
                }
                else throw new PaymentMissingFieldException("Payment Confirmation", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 3)
                    {
                        p.CommissionsExpenses = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Commissions\\Expenses", i);
                }
                else throw new PaymentMissingFieldException("Commissions\\Expenses", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 20)
                    {
                        p.SWIFTBeneficiaryBank = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("SWIFT Beneficiary Bank", i);
                }
                else throw new PaymentMissingFieldException("SWIFT Beneficiary Bank", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240 && Regex.IsMatch(currentField, IBANRegex))
                    {
                        p.BeneficiaryAccount = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Beneficiary Account", i);
                }
                else throw new PaymentMissingFieldException("Beneficiary Account", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240)
                    {
                        p.BeneficiaryName = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Beneficiary Name", i);
                }
                else throw new PaymentMissingFieldException("Beneficiary Name", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (Regex.IsMatch(currentField, @"[A-Z]{2}"))
                    {
                        p.BeneficiaryBankCountry = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Beneficiary Bank Country", i);
                }
                else throw new PaymentMissingFieldException("Beneficiary Bank Country", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240)
                    {
                        p.BeneficiaryBankAddress = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Beneficiary Bank Address", i);
                }
                else throw new PaymentMissingFieldException("Beneficiary Bank Address", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240)
                    {
                        p.BeneficiaryBank = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Beneficiary Bank", i);
                }
                else throw new PaymentMissingFieldException("Beneficiary Bank", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (Regex.IsMatch(currentField, @"[A-Z]{2}"))
                    {
                        p.BeneficiaryCountry = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Beneficiary Country", i);
                }
                else throw new PaymentMissingFieldException("Beneficiary Country", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240)
                    {
                        p.MessageToBeneficiary = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Message To Beneficiary", i);
                }
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (Regex.IsMatch(currentField, @"[A-Z]{2}"))
                    {
                        p.PartnerCountry = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Partner Country", i);
                }
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 20 && float.Parse(currentField) > 0)
                    {
                        p.Amount = float.Parse(currentField);
                    }
                    else throw new PaymentBadFieldFormatException("Amount", i);
                }
                else throw new PaymentMissingFieldException("Amount", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 20)
                    {
                        p.StatisticalCode = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Statistical Code", i);
                }
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 240)
                    {
                        p.Explanations = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Explanations", i);
                }
                else throw new PaymentMissingFieldException("Explanations", i);
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    string[] dateparts = currentField.Split('.');
                    if (!(dateparts.Length > 3))
                    {
                        DateTime date;
                        try
                        {
                            date = new DateTime(int.Parse(dateparts[2]), int.Parse(dateparts[1]), int.Parse(dateparts[0]));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            throw new PaymentBadFieldFormatException("Delivery Date", i);
                        }
                        p.DeliveryDate = date;
                    }
                    else throw new PaymentBadFieldFormatException("Delivery Date", i);
                }
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    if (currentField.Length <= 20)
                    {
                        p.DebtRegisterNumber = currentField;
                    }
                    else throw new PaymentBadFieldFormatException("Debt Register Number", i);
                }
                currentField = fields[currentIndex++];

                if (currentField != "")
                {
                    string[] dateparts = currentField.Split('.');
                    if (!(dateparts.Length > 3))
                    {
                        DateTime date;
                        try
                        {
                            date = new DateTime(int.Parse(dateparts[2]), int.Parse(dateparts[1]), int.Parse(dateparts[0]));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            throw new PaymentBadFieldFormatException("Debt Register Date", i);
                        }
                        p.DebtRegisterDate = date;
                    }
                    else throw new PaymentBadFieldFormatException("Debt Register Date", i);
                }

                payments.Add(p);

                i++;
            }
            
            return payments;
        }
    }
}
