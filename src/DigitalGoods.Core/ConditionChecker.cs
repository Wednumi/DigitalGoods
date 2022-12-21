using DigitalGoods.Core.Attributes;
using System.Linq.Expressions;
using System.Reflection;

namespace DigitalGoods.Core
{
    internal static class ConditionChecker
    {
        internal static bool Check(object instance)
        {
            var properties  = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                if(CheckProperty(property, instance) is false)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckProperty(PropertyInfo propertyInfo, object instance)
        {
            var conditionAttribute = propertyInfo.GetCustomAttribute(typeof(MustNotBeAttribute));
            if(conditionAttribute is null)
            {
                return true;
            }

            var condition = typeof(MustNotBeAttribute)
                .GetProperty(nameof(MustNotBeAttribute.Condition));
            var conditionValues = (object?[])condition!.GetValue(conditionAttribute)!;

            var propertyValue = propertyInfo.GetValue(instance);

            foreach(var value in conditionValues)
            {
                if(propertyValue == value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
