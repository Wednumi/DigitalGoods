using DigitalGoods.Core.Interfaces;

namespace DigitalGoods.Core.Services
{
    public abstract class RollBackableService
    {
        protected IRollBackContainer RollBackContainer;

        public RollBackableService(IRollBackContainer rollBackContainer)
        {
            RollBackContainer = rollBackContainer;
            RollBackContainer.Add(RollBackAsync);
        }

        protected abstract Task RollBackAsync();
    }
}
