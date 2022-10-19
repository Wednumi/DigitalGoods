namespace DigitalGoods.Core.Entities
{
    public class Source : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public int? ParentId { get; private set; }

        public Source? Parent { get; private set; }

        public ICollection<Source>? Childs { get; private set; }

        private Source()
        { }

        public Source(string name, Source? parent = null)
        {
            Name = name;
            Parent = parent;
            ParentId = Parent?.Id;
        }
    }
}
