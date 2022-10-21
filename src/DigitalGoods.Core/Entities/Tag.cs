namespace DigitalGoods.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public int SourceId { get; set; }

        public Source Source { get; private set; } = null!;

        private Tag()
        { }

        public Tag(string name, Source source)
        {
            Name = name;
            SourceId = source.Id;
            Source = source;
        }
    }
}
