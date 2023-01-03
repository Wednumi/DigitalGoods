using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;

namespace DigitalGoods.Core.Services
{
    public class PaymentManager
    {
        private IPaymentService _paymentService;

        private PaymentRegistrator _registrator;

        public PaymentManager(PaymentRegistrator registrator)
        {
            _registrator = registrator;
        }

        public void SetPaymentService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<bool> TryPerformLoggedTransferAsync(User buyer, float price)
        {
            var paymentRecord = new PaymentRecord(buyer, DateTime.Now, price,
                _paymentService.PayMethod, true);
            await _registrator.CreateRecordAsync(paymentRecord);

            var transferResult = await _paymentService.PerformTransfer();

            await _registrator.FinishRecordAsync(transferResult);
            return transferResult;
        }
    }
}
