using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class OfferService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Offer> _offerRepository;

        private readonly MediaService _mediaService;

        private Offer? _previousOffer;

        private Offer? _offer;

        public OfferService(IRepositoryFactory repositoryFactory, MediaService mediaService)
        {
            _repositoryFactory = repositoryFactory;
            _offerRepository = _repositoryFactory.CreateRepository<Offer>();
            _mediaService = mediaService;
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
            _previousOffer = _offer?.GetCopy();
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
                await _mediaService.RollBack();
                return new ActionResult(false, e.Message);
            }
        }

        public async Task<ICollection<Offer>> OfferByUser(User user)
        {
            var specification = new OfferByUserSpec(user);
            var offers = await _offerRepository.ListAsync(specification);
            return offers;
        }

        public async Task<ActionResult> RegisterMedia(Media media, Func<FileStream, Task> saveAction)
        {
            media.SetOffer(_offer!);
            return await _mediaService.Save(media, saveAction);
        }

        public async Task<List<Media>> GetMedias()
        {
            return await _mediaService.InitializedMedias(_offer!);
        }

        public async Task Rollback()
        {
            await _mediaService.RollBack();
            await RollBackOffer();
        }

        private async Task RollBackOffer()
        {
            if(_previousOffer is null)
            {
                await _offerRepository.DeleteAsync(_offer!);
            }
            else
            {
                _offer!.Map(_previousOffer);
                await _offerRepository.UpdateAsync(_offer);
            }
        }
    }
}
