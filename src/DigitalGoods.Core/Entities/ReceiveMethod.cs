namespace DigitalGoods.Core.Entities
{
    public class ReceiveMethod : BaseEntity
    {
        public string Method { get; private set; } = null!;

        private ReceiveMethod() { }

        public ReceiveMethod(string method)
        {
            Method = method;
        }
    }
}
