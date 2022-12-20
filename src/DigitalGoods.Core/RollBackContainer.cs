using DigitalGoods.Core.Interfaces;

namespace DigitalGoods.Core
{
    public class RollBackContainer : IRollBackContainer
    {
        private Func<Task>? _rollBacks;

        public RollBackContainer()
        { }

        public void Add(Func<Task> rollBack)
        {
            ResetIfExist(rollBack);
            _rollBacks += rollBack;
        }

        private void ResetIfExist(Func<Task> rollBack)
        {
            if (AlreadyExist(rollBack))
            {
                Clear();
            }
        }

        private bool AlreadyExist(Func<Task> rollBack)
        {
            var exist = GetDelegates().Where(d => d.Method == rollBack.Method).Any();
            return exist;
        }

        private void Clear()
        {
            _rollBacks = null;
        }

        public async Task RollBack()
        {
            if(GetDelegates().Length > 0)
            {
                await _rollBacks!.Invoke();
            }
        }  
        
        public Delegate[] GetDelegates()
        {
            return _rollBacks is null
                ? new Delegate[0]
                : _rollBacks.GetInvocationList();
        }
    }
}
