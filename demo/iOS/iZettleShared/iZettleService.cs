using System;
using System.Threading.Tasks;
using Foundation;
using iZettle;
using iZettleShared;
using iZettleShared.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(iZettleService))]
namespace iZettleShared.iOS
{
    public class iZettleService : IiZettleService
    {
        readonly UIViewController viewController;

        public iZettleService()
        {
            viewController = Acr.Support.iOS.Extensions.GetTopViewController(UIApplication.SharedApplication);
        }

        TaskCompletionSource<PaymentInfo> chargeAmountTcs;
        public Task<PaymentInfo> ChargeAmountAsync(double amount, string currency, string reference)
        {
            chargeAmountTcs?.TrySetCanceled();
            chargeAmountTcs = new TaskCompletionSource<PaymentInfo>();

            var nativeAmount = new NSDecimalNumber(amount.ToString());

            iZettleSDK.Shared.ChargeAmount(nativeAmount, true, reference, viewController, (iZettleSDKPaymentInfo paymentInfo, NSError error) =>
            {
                if (error != null)
                {
                    //TODO Set descriptive exception
                    chargeAmountTcs.TrySetException(new Exception(error.Domain));
                    return;
                }

                if (paymentInfo == null)
                {
                    chargeAmountTcs.TrySetCanceled();
                    return;
                }

                chargeAmountTcs.TrySetResult(paymentInfo.ToPcl());
            });

            return chargeAmountTcs.Task;
        }
    }

    public static class PaymentInfoExtensions
    {
        public static PaymentInfo ToPcl(this iZettleSDKPaymentInfo paymentInfo)
        {
            return new PaymentInfo
            {
                AID = paymentInfo.AID,
                Amount = paymentInfo.Amount.DoubleValue,
                ApplicationName = paymentInfo.ApplicationName,
                AuthorizationCode = paymentInfo.AuthorizationCode,
                CardBrand = paymentInfo.CardBrand,
                //TODO Dictionary = paymentInfo.Dictionary.Select()
                EntryMode = paymentInfo.EntryMode,
                GratuityAmount = paymentInfo.GratuityAmount.DoubleValue,
                InstallmentAmount = paymentInfo.InstallmentAmount.DoubleValue,
                NumberOfInstallments = paymentInfo.NumberOfInstallments.Int32Value,
                ObfuscatedPan = paymentInfo.ObfuscatedPan,
                PanHash = paymentInfo.PanHash,
                ReferenceNumber = paymentInfo.ReferenceNumber,
                TSI = paymentInfo.TSI,
                TVR = paymentInfo.TVR
            };
        }
    }
}
