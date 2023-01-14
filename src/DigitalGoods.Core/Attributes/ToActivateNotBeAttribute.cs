using System.Linq.Expressions;

namespace DigitalGoods.Core.Attributes
{
    internal class ToActivateNotBeAttribute : Attribute
    {
        public object?[] Condition { get; set; }

        public ToActivateNotBeAttribute(params object?[] condition)
        {
            Condition = condition;
        }
    }
}
