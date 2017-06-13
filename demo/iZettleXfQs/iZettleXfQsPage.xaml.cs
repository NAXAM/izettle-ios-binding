using System;
using System.Threading.Tasks;
using iZettleShared;
using Xamarin.Forms;

namespace iZettleXfQs
{
    public partial class iZettleXfQsPage : ContentPage
    {
        public iZettleXfQsPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
			var service = DependencyService.Get<IiZettleService>();

            await service.ChargeAmountAsync(133, null, Guid.NewGuid().ToString())
                   .ContinueWith(ChargeFinished).ConfigureAwait(true);
        }

        void ChargeFinished(Task<PaymentInfo> task)
        {
            Device.BeginInvokeOnMainThread(() => {
				if (task.IsFaulted)
				{
					DisplayAlert("ERROR", task.Exception.InnerException?.Message ?? task.Exception.Message, "Ok");

					return;
				}

				if (task.IsCanceled)
				{
					DisplayAlert("INFO", "Payment is cancelled", "Ok");

					return;
				}

				DisplayAlert("INFO", $"Payment completed: {task.Result.ReferenceNumber}", "Ok");
            });
        }
    }
}
