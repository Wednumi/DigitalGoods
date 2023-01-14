using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OrderForViewingSpec : Specification<Order>
    {
        public OrderForViewingSpec(User user)
        {
            Query.Where(o => o.Buyer.Equals(user))
                .Include(o => o.Offer)
                    .ThenInclude(of => of.Medias.Where(m => m.IsPreview))
                .Include(o => o.Offer)
                    .ThenInclude(of => of.User);
        }
    }
}
