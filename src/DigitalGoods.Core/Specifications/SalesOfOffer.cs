using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class SalesOfOffer : Specification<Order>
    {
        public SalesOfOffer(Offer offer)
        {
            Query.Where(s => s.Offer!.Equals(offer));
        }
    }
}
