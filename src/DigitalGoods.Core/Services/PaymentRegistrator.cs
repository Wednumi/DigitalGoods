using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace DigitalGoods.Core.Services
{
    public class PaymentRegistrator
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private readonly IRepository<PaymentRecord> _repository;

        private ILogger _logger;

        private PaymentRecord _record;

        public PaymentRegistrator(IRepositoryFactory repositoryFactory, ILogger<PaymentRegistrator> logger)
        {
            _repositoryFactory = repositoryFactory;
            _repository = _repositoryFactory.CreateRepository<PaymentRecord>();
            _logger = logger;
        }

        public async Task CreateRecordAsync(PaymentRecord payment)
        {
            await _repository.UpdateAsync(payment);
            _record = payment;
        }

        public async Task FinishRecordAsync(bool success)
        {
            if (success)
            {
                await ConfirmRecordAsync();
            }
            else
            {
                await CancelRecordAsync();
            }
        }

        private async Task ConfirmRecordAsync()
        {
            try
            {
                _record.Finished = true;
                await _repository.UpdateAsync(_record);
            }
            catch
            {
                _logger.LogCritical($"Payment record [id={_record.Id}] was not confirmed");
            }
        }

        public async Task CancelRecordAsync()
        {
            await _repository.DeleteAsync(_record);
        }
    }
}
