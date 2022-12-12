using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    internal class OfferForEditingSpec : Specification<Offer>
    {
        internal OfferForEditingSpec(User owner, int id)
        {
            Query.Where(o => o.Id == id && o.UserId.Equals(owner.Id))
                .Include(o => o.Medias)
                .Include(o => o.OfferChanges)
                .Include(o => o.Category)
                    .ThenInclude(c => c!.Parent)
                .Include(o => o.Category)
                    .ThenInclude(c => c!.Childs)
                .Include(o => o.Tags);
        }
    }
}
