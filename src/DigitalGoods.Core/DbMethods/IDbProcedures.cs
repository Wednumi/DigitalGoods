namespace DigitalGoods.Core.DbMethods
{
    public interface IDbProcedures
    {
        public static void ReserveActivationCode(int offerId, int orderId)
            => throw new NotSupportedException();
    }
}
