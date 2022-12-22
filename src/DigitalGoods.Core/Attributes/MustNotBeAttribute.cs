using System.Linq.Expressions;

namespace DigitalGoods.Core.Attributes
{
    internal class MustNotBeAttribute : Attribute
    {
        public object?[] Condition { get; set; }

        public MustNotBeAttribute(params object?[] condition)
        {
            Condition = condition;
        }
    }
}
