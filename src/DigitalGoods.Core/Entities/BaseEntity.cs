namespace DigitalGoods.Core.Entities
{
    public abstract class BaseEntity 
    {
        public virtual int Id { get; private set; }

        public override bool Equals(object? obj)
        {
            var sameType = this.GetType() == obj?.GetType();
            if (sameType)
            {
                return this.Id == ((BaseEntity)obj!).Id;
            }
            return false;
        }
    }
}