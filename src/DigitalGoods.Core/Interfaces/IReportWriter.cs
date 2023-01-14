using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Interfaces
{
    public interface IReportWriter
    {
        public Task<string> GenerateOrderReport(Order order);

        public Task<string> GenerateSalesReport(List<Offer> offers);
    }
}
