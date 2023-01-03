using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Specifications;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.DbMethods.ProcedureSpecifications;

namespace DigitalGoods.Core.Services
{
    public class FeedbackService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<Comment> _commentRepository;

        public FeedbackService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _orderRepository = _repositoryFactory.CreateRepository<Order>();
            _commentRepository = _repositoryFactory.CreateRepository<Comment>();
        }

        public async Task UpdateRatingAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
            var updateOfferRatingProc = new UpdateOfferRateProc(order.OfferId);
            await _orderRepository.ExecuteProcedureAsync(updateOfferRatingProc);
        }

        public async Task SendComment(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task<List<Comment>> GetComments(Order order)
        {
            return await _commentRepository.ListAsync(new CommentsForOrderSpec(order));
        }
    }
}
