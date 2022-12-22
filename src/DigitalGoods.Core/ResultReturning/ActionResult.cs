namespace DigitalGoods.Core.ResultReturning
{
    public class ActionResult
    {
        public bool Success { get; set; }

        public bool Failed => !Success;

        public string? Message { get; set; }

        public ActionResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
