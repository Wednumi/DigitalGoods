using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    public class OfferForViewingSpec : Specification<Offer>
    {
        public OfferForViewingSpec(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.Medias)
                .Include(o => o.Category)
                .Include(o => o.Tags)
                .Include(o => o.Sales)
                .Include(o => o.User);
        }
    }
}
