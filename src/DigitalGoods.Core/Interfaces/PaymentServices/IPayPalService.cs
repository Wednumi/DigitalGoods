namespace DigitalGoods.Core.Interfaces.PaymentServices
{
    public interface IPayPalService : IPaymentService
    {
        public Task<bool> Login(string userName, string password);
    }
}
