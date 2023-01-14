using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    public class OfferForReportSpec : Specification<Offer>
    {
        public OfferForReportSpec(User owner)
        {
            Query.Where(o => o.UserId.Equals(owner.Id) && o.Sales.Count() > 0)
                .Include(o => o.Sales);
        }
    }
}
