using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Core.Services;

namespace DigitalGoods.Infrastructure.ReportWriters
{
    public class PdfReportWriter : IReportWriter
    {
        private static string _rootPath = @"reports";

        private static string dateToStringFormat = "MM-dd-yyyy";

        private string _externalPath;

        private string _internalPath;

        private ActivationCodeService _activationCodeService;

        public PdfReportWriter(ActivationCodeService activationCodeService)
        {
            _activationCodeService = activationCodeService;
            _externalPath = Path.Combine(Environment.CurrentDirectory,
                "wwwroot");
        }

        public async Task<string> GenerateOrderReport(Order order)
        {
            await PrepareDirectory();
            return await CreatePdfOder(order);
        }

        private async Task PrepareDirectory()
        {
            SetInternalPath();
            var fullPath = Path.Combine(_externalPath, _internalPath);
            await Task.Run(() => Directory.CreateDirectory(fullPath));
            await DeleteOutDatedFilesAsync();
        }

        private void SetInternalPath()
        {
            _internalPath = Path.Combine(_rootPath, 
                DateTime.Now.ToString(dateToStringFormat));
        }

        private async Task DeleteOutDatedFilesAsync()
        {
            var folders = Directory.GetDirectories(Path.Combine(_externalPath, _rootPath));
            var currentDateString = DateTime.Now.ToString(dateToStringFormat);
            var outDated = folders.Where(f => !f.Contains(currentDateString)).ToList();
            await Task.Run(() => outDated.ForEach(d => Directory.Delete(d, true)));
        }

        private async Task<string> CreatePdfOder(Order order)
        {
            var filePath = Path.Combine(_internalPath, "Order" + order.Id + ".pdf");
            var fullPath = Path.Combine(_externalPath, filePath);
            var document = GetDocument(fullPath);
            document
                .SetFontSize(14)
                .SetMargins(10, 10, 5, 15);

            Paragraph header = new Paragraph($"Order #{order.Id}")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            document.Add(header);

            AddText(document, $"Product bought: {order.Offer.Name}");
            AddText(document,$"Paid: {order.Offer.FinalPrice()}$");
            AddText(document,$"Date: {order.Date.ToString()}");
            AddText(document,$"Buyer: {order.Buyer.UserName}; {order.Buyer.Email}");
            AddText(document, $"Receive method: {order.Offer.ReceiveMethod.ToString()}");

            if(order.Offer.ReceiveMethod == Core.Enums.ReceiveMethod.ActivationCode)
            {
                var code = await _activationCodeService.GetByOrder(order);
                AddText(document, $"Activation code: {code.Code}");
            }
            document.Close();

            return filePath;
        }

        private Document GetDocument(string filePath)
        {
            PdfWriter writer = new PdfWriter(filePath);
            PdfDocument pdf = new PdfDocument(writer);
            return new Document(pdf);
        }

        private void AddText(Document document, string text)
        {
            Paragraph paragraph = new Paragraph(text);
            document.Add(paragraph);
        }

        public async Task<string> GenerateSalesReport(List<Offer> offers)
        {
            await PrepareDirectory();
            return await Task.Run(() => CreateSalesReport(offers));
        }

        private string CreateSalesReport(List<Offer> offers)
        {
            var filePath = Path.Combine(_internalPath, "SalesReport" + offers[0].UserId + ".pdf");
            var fullPath = Path.Combine(_externalPath, filePath);
            var document = GetDocument(fullPath);
            document
                .SetFontSize(14)
            .SetMargins(10, 10, 5, 15);

            Paragraph header = new Paragraph($"Sales report")
                .SetFontSize(20);
            document.Add(header);

            float[] pointColumnWidths = { 150F, 150F, 150F, 150F };
            Table table = new Table(pointColumnWidths);
            table.AddCell("Name");
            table.AddCell("Price");
            table.AddCell("Sold amount");
            table.AddCell("Total");

            float userIncome = 0;
            foreach (var offer in offers)
            {
                table.AddCell(offer.Name);
                table.AddCell(offer.FinalPrice().ToString());
                table.AddCell(offer.Sales.Count().ToString());
                float total = offer.FinalPrice() * offer.Sales.Count();
                userIncome += total;
                table.AddCell(total.ToString());
            }
            document.Add(table);

            AddText(document, $"Total income: {userIncome}");
     
            document.Close();

            return filePath;
        }
    }
}
