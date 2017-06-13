using System.Collections.Generic;
using System.Threading.Tasks;

namespace iZettleShared
{
    public class PaymentInfo
    {
        public Dictionary<string, object> Dictionary { get; set; }

        public double Amount { get; set; }

        public double GratuityAmount { get; set; }

        public string ReferenceNumber { get; set; }

        public string EntryMode { get; set; }

        public string AuthorizationCode { get; set; }

        public string ObfuscatedPan { get; set; }

        public string PanHash { get; set; }

        public string CardBrand { get; set; }

        public string AID { get; set; }

        public string TSI { get; set; }

        public string TVR { get; set; }

        public string ApplicationName { get; set; }

        public int NumberOfInstallments { get; set; }

        public double InstallmentAmount { get; set; }
    }

    public interface IiZettleService
    {
        Task<PaymentInfo> ChargeAmountAsync(double amount, string currency, string reference);
    }
}
