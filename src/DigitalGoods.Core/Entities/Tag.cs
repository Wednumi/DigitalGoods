namespace DigitalGoods.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = null!;

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Offer> Offers { get; set; } = new List<Offer>();

        private Tag()
        { }

        public Tag(string name, Category? category)
        {
            Name = name;
            CategoryId = category?.Id;
            Category = category;
        }
    }
}
