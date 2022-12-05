using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class OfferService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Offer> _offerRepository;

        private Offer? _offer;

        public OfferService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _offerRepository = _repositoryFactory.CreateRepository<Offer>();
        }

        public async Task<Offer> GetVerifiedOffer(User owner, int? offerId)
        {
            if (offerId.HasValue)
            {
                _offer = await _offerRepository.GetByIdAsync((int)offerId);
                if (OwnerFaked(owner))
                {
                    _offer = null;
                }
            }
            _offer ??= new Offer(owner);
            return _offer;
        }

        private bool OwnerFaked(User owner)
        {
            return _offer is not null && !_offer.IsOwnerValid(owner);
        }

        public async Task<ActionResult> Save()
        {
            try
            {
                await _offerRepository.UpdateAsync(_offer!);
                return new ActionResult(true);
            }
            catch(Exception e)
            {
                return new ActionResult(false, e.Message);
            }
        }

        public async Task<ICollection<Offer>> OfferByUser(User user)
        {
            var specification = new OfferByUserSpec(user);
            var offers = await _offerRepository.ListAsync(specification);
            return offers;
        }
    }
}
