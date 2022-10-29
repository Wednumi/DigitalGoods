namespace DigitalGoods.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int OrderId { get; private set; }

        public Order Order { get; private set; } = null!;

        public string Text { get; private set; } = null!;

        private Comment() { }

        public Comment(Order order, string text)
        {
            Order = order;
            Text = text;
        }
    }
}
