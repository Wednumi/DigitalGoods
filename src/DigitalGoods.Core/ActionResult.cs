namespace DigitalGoods.Core
{
    public class ActionResult
    {
        public bool Success { get; set; }

        public bool Failed => !Success;

        public string? Error { get; set; }

        public ActionResult(bool success, string? error = null)
        {
            Success = success;
            Error = error;
        }
    }
}
