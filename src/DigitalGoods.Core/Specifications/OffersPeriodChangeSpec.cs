using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OffersPeriodChangeSpec : Specification<Offer>
    {
        public OffersPeriodChangeSpec(int periodDays)
        {
            Query.Where(o => IDbFunctions.SoldPeriodChange(o.Id, periodDays) > 0)
                .OrderByDescending(o => IDbFunctions.SoldPeriodChange(o.Id, periodDays))
                .Include(o => o.Sales)
                .Include(o => o.Medias.Where(m => m.IsPreview));
        }
    }
}
