using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OffersHotChoice : Specification<Offer>
    {
        public OffersHotChoice()
        {
            Query.Where(o => o.State == Enums.OfferState.Active && o.Sales.Count > 0)
                .OrderByDescending(o => IDbFunctions.WeeklySalesPerDay(o.Id))
                .Include(o => o.Medias.Where(m => m.IsPreview))
                .Include(o => o.Sales);
        }
    }
}
