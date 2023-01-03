using DigitalGoods.Core.Entities;
using Ardalis.Specification;

namespace DigitalGoods.Core.Specifications
{
    internal class ActivationCodeOfOfferSpec : Specification<ActivationCode>
    {
        public ActivationCodeOfOfferSpec(Offer offer)
        {
            Query.Where(c => c.OfferId == offer.Id);
        }
    }
}
