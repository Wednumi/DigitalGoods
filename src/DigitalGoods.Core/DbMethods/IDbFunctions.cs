using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.DbMethods
{
    public interface IDbFunctions
    {
        public static float FinalPrice(float price, int discount)
            => throw new NotSupportedException();

        public static float WeeklySalesPerDay(int offerId)
            => throw new NotSupportedException();

        public static float SoldPeriodChange(int offerId, int periodDays)
            => throw new NotSupportedException();
    }
}