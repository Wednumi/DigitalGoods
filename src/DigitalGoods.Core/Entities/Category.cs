namespace DigitalGoods.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public int? ParentId { get; private set; }

        public Category? Parent { get; private set; }

        public ICollection<Category>? Childs { get; private set; }

        private Category()
        { }

        public Category(string name, Category? parent = null)
        {
            Name = name;
            Parent = parent;
            ParentId = Parent?.Id;
        }
    }
}
