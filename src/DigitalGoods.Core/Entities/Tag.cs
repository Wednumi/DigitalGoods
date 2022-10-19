namespace DigitalGoods.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public Source Source { get; private set; } = null!;

        private Tag()
        { }

        public Tag(string name, Source source)
        {
            Name = name;
            Source = source;
        }
    }
}
