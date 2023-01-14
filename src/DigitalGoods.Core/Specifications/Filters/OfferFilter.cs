using DigitalGoods.Core.Entities;
using System.Linq.Expressions;

namespace DigitalGoods.Core.Specifications.Filters
{
    public class OfferFilter
    {
        public string? Name { get; set; }

        public float? BottomPrice { get; set; }

        public float? TopPrice { get; set; }

        public ICollection<Category>? CategoryTree { get; set; }

        public ICollection<Tag>? Tags { get; set; }

        public List<Expression<Func<Offer, object?>>> OrderExpressions { get; set; }
    }
}
