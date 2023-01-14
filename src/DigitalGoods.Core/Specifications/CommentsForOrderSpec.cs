using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    internal class CommentsForOrderSpec : Specification<Comment>
    {
        public CommentsForOrderSpec(Order order)
        {
            Query.Where(c => c.Order.Equals(order));
        }
    }
}
