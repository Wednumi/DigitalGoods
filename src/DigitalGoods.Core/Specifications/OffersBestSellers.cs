using Ardalis.Specification;
using DigitalGoods.Core.DbMethods;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OffersBestSellers : Specification<Offer>
    {
        public OffersBestSellers()
        {
            var earliestPossibleDate = DateTime.Now - new TimeSpan(days: 90, 0, 0, 0);
            Query.Where(o => o.State == Enums.OfferState.Active && o.Sales.Count > 0)
                .OrderByDescending(o =>
                    o.Sales.Where(order => order.Date > earliestPossibleDate).Count()
                    * Math.Pow((double)o.Price, 0.3))
                .Include(o => o.Medias.Where(m => m.IsPreview))
                .Include(o => o.Sales);
        }
    }
}
