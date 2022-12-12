namespace DigitalGoods.Core.Entities
{
    public class Media : BaseEntity
    {
        public string FileName { get; set; } = null!;

        public string Path { get; set; } = null!;

        public string ContentType { get; set; } = null!;

        public long Size { get; set; }

        public int? OfferId { get; private set; }

        public Offer? Offer { get; private set; }

        public bool IsPreview { get; set; }

        private Media() 
        { }

        public Media(string fileName, string contentType, long size)
        {
            IsPreview = false;
            FileName = fileName;
            ContentType = contentType;
            Size = size;
        }

        public void SetOffer(Offer offer)
        {
            Offer = offer;
            OfferId = offer.Id;
        }
    }
}
