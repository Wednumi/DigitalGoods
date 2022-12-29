using DigitalGoods.Core.Entities;
using System.Linq.Expressions;

namespace DigitalGoods.Web.BlazorServer.Pages.Search.OrderPicking
{
    public class OrderItem
    {
        public bool Included { get; set; }

        public string Name { get; set; }

        public Expression<Func<Offer, object?>> Expression { get; set; }

        public OrderItem(string name, Expression<Func<Offer, object?>> expression)
        {
            Name = name;
            Expression = expression;
            Included = false;
        }

        public void ToggleIncluded()
        {
            Included = !Included;
        }
    }
}
