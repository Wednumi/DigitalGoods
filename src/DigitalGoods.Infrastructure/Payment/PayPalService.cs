using DigitalGoods.Core.Enums;
using DigitalGoods.Core.Interfaces.PaymentServices;

namespace DigitalGoods.Infrastructure.Payment
{
    public class PayPalService : IPayPalService
    {
        public PayMethod PayMethod
        {
            get { return PayMethod.PayPal; }
        }

        public async Task<bool> Login(string userName, string password)
        {
            return true;
        }

        public async Task<bool> PerformTransfer()
        {
            return true;
        }
    }
}
