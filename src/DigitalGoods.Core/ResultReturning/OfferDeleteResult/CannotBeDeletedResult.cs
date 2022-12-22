namespace DigitalGoods.Core.ResultReturning.OfferDeleteResult
{
    internal class CannotBeDeletedResult : ActionResult
    {
        public CannotBeDeletedResult()
            :base(false, "The offer cannot be deleted if it has already been sold")
        {
        }
    }
}
