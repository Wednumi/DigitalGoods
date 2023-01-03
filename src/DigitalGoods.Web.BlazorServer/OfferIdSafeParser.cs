using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Specifications;
using DigitalGoods.Core.Interfaces;

namespace DigitalGoods.Web.BlazorServer
{
    public class OfferIdParser
    {
        IRepositoryFactory _repositoryFactory;

        public OfferIdParser(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Offer?> Parse(string idString)
        {
            var stringParseResult = int.TryParse(idString, out int parsedId);
            if(stringParseResult is false)
            {
                return null;
            }
            return await GetOffer(parsedId);
        }

        private async Task<Offer?> GetOffer(int id)
        {
            var repos = _repositoryFactory.CreateRepository<Offer>();
            return await repos.FirstOrDefaultAsync(new OfferForViewingSpec(id));
        }
    }
}
