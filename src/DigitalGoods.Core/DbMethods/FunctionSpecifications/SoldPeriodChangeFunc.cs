namespace DigitalGoods.Core.DbMethods.FunctionSpecifications
{
    public class SoldPeriodChangeFunc : FunctionSpecification<float>
    {
        private string _offerId;

        private int _period;

        public SoldPeriodChangeFunc(int offerId, int period)
        {
            Function = () => IDbFunctions.SoldPeriodChange(offerId, period);
            _offerId = offerId.ToString();
            _period = period;
        }

        public override FormattableString ExecQuery()
        {
            return $"SELECT dbo.f_period_change({_offerId}, {_period})";
        }
    }
}
