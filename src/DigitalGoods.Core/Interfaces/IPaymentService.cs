using DigitalGoods.Core.Enums;

namespace DigitalGoods.Core.Interfaces
{
    public interface IPaymentService
    {
        public PayMethod PayMethod { get; }

        public Task<bool> PerformTransfer();
    }
}
