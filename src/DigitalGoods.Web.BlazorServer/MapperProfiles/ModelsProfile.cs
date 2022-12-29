using AutoMapper;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Extensions;
using DigitalGoods.Web.BlazorServer.Models;

namespace DigitalGoods.Core.MapperProfiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<OfferModel, Offer>().IgnoreNoMap();
            CreateMap<Offer, OfferModel>().IgnoreNoMap();
            CreateMap<CategoryTagsRelated, Offer>().IgnoreNoMap();
            CreateMap<TagModel, Tag>().IgnoreNoMap();
        }
    }
}
