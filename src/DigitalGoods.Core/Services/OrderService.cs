﻿using DigitalGoods.Core.Entities;
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

        public async Task<bool> PerformOrderAsync(Offer offer, User buyer)
        {
            var paymentResult = await _paymentManager.TryPerformLoggedTransferAsync(buyer, offer.FinalPrice());

            if (paymentResult is false)
            {
                return false;
            }

            var order = new Order(offer, buyer, DateTime.Now);
            await _orderRepository.UpdateAsync(order);
            offer.Amount--;
            await _offerRepository.UpdateAsync(offer);

            if (offer.ReceiveMethod == ReceiveMethod.ActivationCode)
            {
                await ReserveActivationCodeAsync(offer.Id, order.Id);
            }
            return true;
        }

        private async Task ReserveActivationCodeAsync(int offerId, int orderId)
        {
            var procedure = new ReserveCodeProc(offerId, orderId);
            await _orderRepository.ExecuteProcedureAsync(procedure);
        }
    }
}
