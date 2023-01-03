using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class OrderViewingService
    {
        private IRepositoryFactory _repositoryFactory;

        private IRepository<Order> _repository;

        public OrderViewingService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _repository = repositoryFactory.CreateRepository<Order>();
        }

        public async Task<List<Order>> GetOrdersAsync(User user)
        {
            return await _repository.ListAsync(new OrderForViewingSpec(user));
        }

        public async Task ConfirmReceive(Order order)
        {
            order.ConfirmReceive();
            await _repository.UpdateAsync(order);
            await TransferMoneyToSaller(order.Offer);
        }

        private async Task TransferMoneyToSaller(Offer offer)
        {
            var user = offer.User;
            user.MoneyAccount += offer.FinalPrice();
            var userRepos = _repositoryFactory.CreateRepository<User>();
            await userRepos.UpdateAsync(user);
        }
    }
}
