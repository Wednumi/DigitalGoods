namespace DigitalGoods.Core.Entities
{
    public class BankAccountType : BaseEntity
    {
        public string Name { get; private set; } = null!;

        private BankAccountType()
        { }

        public BankAccountType(string name)
        {
            Name = name;
        }
    }
}
