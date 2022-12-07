using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    internal class MediaByOfferSpec : Specification<Media>
    {
        internal MediaByOfferSpec(Offer offer)
        {
            Query.Where(m => m.OfferId == offer.Id);
        }
    }
}
