namespace DigitalGoods.Core.DbMethods.ProcedureSpecifications
{
    public class ReserveCodeProc : ProcedureSpecification
    {
        private string _offerId;

        private string _orderId;

        public ReserveCodeProc(int offerId, int orderId)
        {
            Procedure = () => IDbProcedures.ReserveActivationCode(offerId, orderId);
            _offerId = offerId.ToString();
            _orderId = orderId.ToString();
        }

        public override FormattableString ExecQuery()
        {
            return $"EXEC dbo.p_reserve_activation_code @OfferId = {_offerId}, @OrderId = {_orderId}";
        }
    }
}
