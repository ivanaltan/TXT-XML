using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT_XML
{
    class Payment
    {
        public static string IBANRegex = @"[A-Z]{2}\d{2}[A-Z\d]{1,30}";

        // [Required]
        // [StringLength(240)]
        // [RegularExpression(IBANRegex)]
        public string SourceAccount { get; set; }

        // [Required]
        // Range[0,999999999999999999.99]
        public double PaymentAmmount { get; set; }

        // [Required]
        // [RegularExpression("S|U")]
        public char Priority { get; set; }

        // [Required]
        // [StringLength(3)]
        public string PaymentMethod { get; set; }

        // [Required]
        // [StringLength(7)]
        public string PaymentConfirmation { get; set; }

        // [Required]
        // [StringLength(3)]
        public string CommissionsExpenses { get; set; }

        // [Required]
        // [StringLength(20)]
        public string SWIFTBeneficiaryBank { get; set; }

        // [Required]
        // [StringLength(240)]
        // [RegularExpression(IBANRegex)]
        public string BeneficiaryAccount { get; set; }

        // [Required]
        // [StringLength(240)]
        public string BeneficiaryName { get; set; }

        // [Required]
        // [StringLength(2)]
        // [RegularExpression(@"[A-Z]{2}")]
        public string BeneficiaryBankCountry { get; set; }

        // [Required]
        // [StringLength(240)]
        public string BeneficiaryBankAddress { get; set; }

        // [Required]
        // [StringLength(240)]
        public string BeneficiaryBank { get; set; }

        // [Required]
        // [StringLength(2)]
        // [RegularExpression(@"[A-Z]{2}")]
        public string BeneficiaryCountry { get; set; }

        // [StringLength(240)]
        public string MessageToBeneficiary { get; set; }

        // [StringLength(2)]
        // [RegularExpression(@"[A-Z]{2}")]
        public string PartnerCountry { get; set; }

        // [Required]
        // Range[0,999999999999999999.99]
        public double Amount { get; set; }

        // [StringLength(20)]
        public string StatisticalCode { get; set; }

        // [Required]
        // [StringLength(240)]
        public string Explanations { get; set; }

        public DateTime DeliveryDate { get; set; }

        // [StringLength(20)]
        public string DebtRegisterNumber { get; set; }
        
        public DateTime DebtRegisterDate { get; set; }

        public Payment()
        {
        }

        public Payment(string sourceAccount, double paymentAmmount, char priority, string paymentMethod, string paymentConfirmation, string commissionsExpenses, string sWIFTBeneficiaryBank, string beneficiaryAccount, string beneficiaryName, string beneficiaryBankCountry, string beneficiaryBankAddress, string beneficiaryBank, string beneficiaryCountry, string messageToBeneficiary, string partnerCountry, string detailsLine1, double amount, string statisticalCode, string explanations, DateTime deliveryDate, string debtRegisterNumber, DateTime debtRegisterDate)
        {
            SourceAccount = sourceAccount;
            PaymentAmmount = paymentAmmount;
            Priority = priority;
            PaymentMethod = paymentMethod;
            PaymentConfirmation = paymentConfirmation;
            CommissionsExpenses = commissionsExpenses;
            SWIFTBeneficiaryBank = sWIFTBeneficiaryBank;
            BeneficiaryAccount = beneficiaryAccount;
            BeneficiaryName = beneficiaryName;
            BeneficiaryBankCountry = beneficiaryBankCountry;
            BeneficiaryBankAddress = beneficiaryBankAddress;
            BeneficiaryBank = beneficiaryBank;
            BeneficiaryCountry = beneficiaryCountry;
            MessageToBeneficiary = messageToBeneficiary;
            PartnerCountry = partnerCountry;
            Amount = amount;
            StatisticalCode = statisticalCode;
            Explanations = explanations;
            DeliveryDate = deliveryDate;
            DebtRegisterNumber = debtRegisterNumber;
            DebtRegisterDate = debtRegisterDate;
        }
    }
}
