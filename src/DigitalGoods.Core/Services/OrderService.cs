using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Enums;
using DigitalGoods.Core.DbMethods.ProcedureSpecifications;
using System;

namespace DigitalGoods.Core.Services
{
    public class OrderService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<Offer> _offerRepository;

        private PaymentManager _paymentManager;

        public OrderService(IRepositoryFactory repositoryFactory, PaymentManager paymentManager)
        {
            _repositoryFactory = repositoryFactory;
            _orderRepository = _repositoryFactory.CreateRepository<Order>();
            _offerRepository = _repositoryFactory.CreateRepository<Offer>();
            _paymentManager = paymentManager;
        }

        public void SetPaymentService(IPaymentService paymentService)
        {
            _paymentManager.SetPaymentService(paymentService);
        }

        public async Task<Order?> PerformOrderAsync(Offer offer, User buyer)
        {
            var decreaseResult  = await DecreaseAmountAsync(offer);

            if (!decreaseResult )
            {
                return null;
            }

            var paymentResult = await _paymentManager.TryPerformLoggedTransferAsync(buyer, offer.FinalPrice());

            if (paymentResult is false)
            {
                await PreviousAmountAsync(offer);
                return null;
            }

            var order = new Order(offer, buyer, DateTime.Now);
            await _orderRepository.UpdateAsync(order);

            if (offer.ReceiveMethod == ReceiveMethod.ActivationCode)
            {
                await ReserveActivationCodeAsync(offer.Id, order.Id);
            }
            return order;
        }

        private async Task<bool> DecreaseAmountAsync(Offer offer)
        {
            var dbRecord = await _offerRepository.GetByIdAsync(offer.Id);
            if(dbRecord!.Amount > 0)
            {
                dbRecord!.Amount--;
                await _offerRepository.UpdateAsync(dbRecord);
                return true;
            }
            return false;
        }

        private async Task PreviousAmountAsync(Offer offer)
        {
            var dbRecord = await _offerRepository.GetByIdAsync(offer.Id);
            dbRecord!.Amount++;
            await _offerRepository.UpdateAsync(dbRecord);
        }

        private async Task ReserveActivationCodeAsync(int offerId, int orderId)
        {
            var procedure = new ReserveCodeProc(offerId, orderId);
            await _orderRepository.ExecuteProcedureAsync(procedure);
        }
    }
}
