using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Specifications;

namespace DigitalGoods.Core.Services
{
    public class ActivationCodeService : RollBackableService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<ActivationCode> _repository;

        private List<ActivationCode> _added;

        public ActivationCodeService(IRepositoryFactory repositoryFactory, IRollBackContainer rollBackContainer)
            : base(rollBackContainer)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<ActivationCode>();
            _added = new List<ActivationCode>();
        }

        public async Task Add(List<ActivationCode> activationCodes)
        {
            await _repository.UpdateRangeAsync(activationCodes);
            _added.AddRange(activationCodes);
        }

        protected async override Task RollBackAsync()
        {
            await _repository.DeleteRangeAsync(_added);
        }
    }
}
