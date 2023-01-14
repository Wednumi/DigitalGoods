namespace DigitalGoods.Core.DbMethods
{
    public interface IDbProcedures
    {
        public static void ReserveActivationCode(int offerId, int orderId)
            => throw new NotSupportedException();

        public static void UpdateOfferRate(int offerId)
            => throw new NotSupportedException();
    }
}
