using AutoMapper;
using DigitalGoods.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace DigitalGoods.Web.BlazorServer.Models
{
    public class OfferModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Too short name")]
        public string? Name { get; set; }

        [Range(1, 2000, ErrorMessage = "We support price in range 1 - 2000$")]
        [DataType(DataType.Currency)]
        public float? Price { get; set; }

        [Range(0, 100, ErrorMessage = "Disount must be in range 0 - 100%")]
        public int Discount { get; set; } = 0;

        public string? Discription { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Amount can't be less than 0")]
        public int Amount { get; set; } = 0;

        public Source? Source { get; set; }

        public ReceiveMethod? ReceiveMethod { get; set; }

        public List<Tag>? Tags { get; set; }

        public List<Media>? Medias { get; set; }
    }
}
