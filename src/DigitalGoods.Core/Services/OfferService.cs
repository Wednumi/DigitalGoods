using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.ResultReturning;
using DigitalGoods.Core.Specifications;
using AutoMapper;

namespace DigitalGoods.Core.Services
{
    public class OfferService : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<Offer> _offerRepository;

        private readonly IMapper _mapper;

        public Offer? _previousOffer { get; set; }

        public Offer Offer { get; set; } = null!;

        public OfferService(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer,
            IMapper mapper)
            : base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _offerRepository = _repositoryFactory.CreateRepository<Offer>();
            _mapper = mapper;
        }

        public async Task InitializeAsync(User owner, int? offerId)
        {
            var retrieved = await RetrieveOfferAsync(owner, offerId);
            _previousOffer = retrieved is not null
                ? _mapper.Map<Offer, Offer>(retrieved)
                : null;
            Offer = retrieved ?? new Offer(owner);
        }

        private async Task<Offer?> RetrieveOfferAsync(User owner, int? offerId)
        {
            if (offerId.HasValue)
            {
                var specification = new OfferForEditingSpec(owner, (int)offerId);
                return await _offerRepository.FirstOrDefaultAsync(specification);
            }
            return null;
        }

        public async Task<ActionResult> SaveAsync()
        {
            try
            {
                Offer.UpdateState();
                await _offerRepository.UpdateAsync(Offer!);
                return new ActionResult(true);
            }
            catch(Exception e)
            {
                return new ActionResult(false, e.Message);
            }
        }

        protected override async Task RollBackAsync()
        {
            if(_previousOffer is null)
            {
                await _offerRepository.DeleteAsync(Offer!);
            }
            else
            {
                _mapper.Map<Offer, Offer>(_previousOffer, Offer);
                await _offerRepository.UpdateAsync(Offer);
            }
        }
    }
}
