using DigitalGoods.Core.Entities;
using Ardalis.Specification;

namespace DigitalGoods.Core.Specifications
{
    internal class ActivationCodeForOrderSpec : Specification<ActivationCode>
    {
        public ActivationCodeForOrderSpec(Order order)
        {
            Query.Where(c => c.Order.Equals(order));
        }
    }
}
