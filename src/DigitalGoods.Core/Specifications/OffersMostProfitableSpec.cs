using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OffersMostProfitableSpec : Specification<Offer>
    {
        public OffersMostProfitableSpec()
        {
            Query.Where(o => o.State == Enums.OfferState.Active)
                .OrderByDescending(o => o.Price - IDbFunctions.FinalPrice((float)o.Price, o.Discount))
                .Include(o => o.Medias.Where(m => m.IsPreview))
                .Include(o => o.Sales);
        }
    }
}
