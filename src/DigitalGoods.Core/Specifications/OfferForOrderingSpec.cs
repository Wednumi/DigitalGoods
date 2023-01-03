using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    public class OfferForOrderingSpec : Specification<Offer>
    {
        public OfferForOrderingSpec(int id)
        {
            Query.Where(o => o.Id == id)
                .Include(o => o.User)
                .Include(o => o.Medias.Where(m => m.IsPreview));
        }
    }
}
