namespace DigitalGoods.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public int CategoryId { get; set; }

        public Category Category { get; private set; } = null!;

        private Tag()
        { }

        public Tag(string name, Category category)
        {
            Name = name;
            CategoryId = category.Id;
            Category = category;
        }
    }
}
