using AutoMapper;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Extensions;

namespace DigitalGoods.Core.MapperProfiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, Offer>().IgnoreNoMap();
        }
    }
}
