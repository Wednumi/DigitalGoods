﻿namespace DigitalGoods.Core.Interfaces
{
    public interface IRollBackContainer
    {
        public void Add(Func<Task> rollBack);

        public Task RollBack();

        public Delegate[] GetDelegates();
    }
}
