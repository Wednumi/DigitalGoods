namespace DigitalGoods.Core.DbMethods.ProcedureSpecifications
{
    public class UpdateOfferRateProc : ProcedureSpecification
    {
        private string _offerId;

        public UpdateOfferRateProc(int offerId)
        {
            Procedure = () => IDbProcedures.UpdateOfferRate(offerId);
            _offerId = offerId.ToString();
        }

        public override FormattableString ExecQuery()
        {
            return $"dbo.p_update_offer_rate @OfferId = {_offerId}";
        }
    }
}
