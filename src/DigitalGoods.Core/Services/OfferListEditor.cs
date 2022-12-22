using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.ResultReturning;
using DigitalGoods.Core.ResultReturning.OfferDeleteResult;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class OfferListEditor
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Offer> _offerRepository;

        private readonly MediaService _mediaService;

        public ICollection<Offer> Offers { get; set; } = null!;

        public OfferListEditor(IRepositoryFactory repositoryFactory, MediaService mediaService)
        {
            _repositoryFactory = repositoryFactory;
            _offerRepository = _repositoryFactory.CreateRepository<Offer>();
            _mediaService = mediaService;
        }

        public async Task InitialiazeAsync(User user)
        {
            Offers = await _offerRepository.ListAsync(new OffersByUserSpec(user));
        }

        public async Task ActivateOfferAsync(Offer offer) 
        { 
            offer.Activate();
            await _offerRepository.UpdateAsync(offer);
        }

        public async Task DeActivateOfferAsync(Offer offer)
        {
            offer.Deactivate();
            await _offerRepository.UpdateAsync(offer);
        }

        public async Task<ActionResult> TryDeleteAsync(Offer offer)
        {
            var alreadySold = await AlreadySold(offer);
            if (alreadySold)
            {
                return new CannotBeDeletedResult();
            }
            try
            {
                await DeleteOfferCascade(offer);
                return new ActionResult(true);
            }
            catch
            {
                return new ActionResult(false);
            }
        }

        private async Task<bool> AlreadySold(Offer offer)
        {
            var orderRepository = _repositoryFactory.CreateRepository<Order>();
            var result = await orderRepository.AnyAsync(new SalesOfOffer(offer));
            return result;
        }

        private async Task DeleteOfferCascade(Offer offer)
        {
            Offers.Remove(offer);
            await _offerRepository.DeleteAsync(offer);
            await _mediaService.DeleteRangeAsync(offer.Medias);
        }
    }
}
