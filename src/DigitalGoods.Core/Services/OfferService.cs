using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class OfferService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Offer> _offerRepository;

        private readonly IRepository<Category> _categoryRepository;

        private readonly MediaService _mediaService;

        private Offer? _previousOffer;

        private Offer? _offer;

        public OfferService(IRepositoryFactory repositoryFactory, MediaService mediaService)
        {
            _repositoryFactory = repositoryFactory;
            _offerRepository = _repositoryFactory.CreateRepository<Offer>();
            _categoryRepository = _repositoryFactory.CreateRepository<Category>();
            _mediaService = mediaService;
        }

        public async Task<Offer> GetConfiguredOffer(User owner, int? offerId)
        {
            if (offerId.HasValue)
            {
                OfferForEditingSpec specification = new (owner, (int)offerId);
                _offer = await _offerRepository.FirstOrDefaultAsync(specification);
            }
            _previousOffer = _offer?.GetCopy();
            _offer ??= new Offer(owner);

            _mediaService.SetMedias(_offer.Medias);

            return _offer;
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
            var specification = new OffersByUserSpec(user);
            var offers = await _offerRepository.ListAsync(specification);
            return offers;
        }

        public async Task<ActionResult> RegisterMedia(Media media, Func<FileStream, Task> saveAction)
        {
            media.SetOffer(_offer!);
            return await _mediaService.Save(media, saveAction);
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

        public async Task DeleteMedia(Media media)
        {
            await _mediaService.Delete(media);
        }
    }
}
