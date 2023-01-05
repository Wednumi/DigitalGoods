using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OrderForRateCount : Specification<Order>
    {
        public OrderForRateCount(Offer offer)
        {
            Query.Where(o => o.Offer.Equals(offer) && o.Rate != null);
        }
    }
}
