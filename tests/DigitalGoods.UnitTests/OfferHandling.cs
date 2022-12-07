using Moq;

namespace DigitalGoods.UnitTests
{
    public class OfferHandling
    {
        [Fact]
        public void OfferService_retrieve_offer_for_owner()
        {
            int offerId = 10;
            var mockSetup = OfferServiceSetup(offerId);
            var owner = mockSetup.Owner;
            var sut = mockSetup.Service;

            var offerResult = sut.GetVerifiedOffer(owner, It.IsAny<int>()).Result;

            Assert.Equal(offerId, offerResult.Id);
        }

        [Fact]
        public void OfferService_capture_faked_owner()
        {
            var fakeOwnerMock = new Mock<User>(It.IsAny<string>(), It.IsAny<string>());
            fakeOwnerMock.Setup(u => u.Id).Returns("faker-id");
            int offerId = 10;
            var mockSetup = OfferServiceSetup(offerId);
            var owner = mockSetup.Owner;
            var sut = mockSetup.Service;

            var offerResult = sut.GetVerifiedOffer(fakeOwnerMock.Object, It.IsAny<int>()).Result;

            Assert.Equal(default, offerResult.Id);
        }

        private (OfferService Service, User Owner) OfferServiceSetup(int storedOfferId)
        {
            var userMock = new Mock<User>(It.IsAny<string>(), It.IsAny<string>());
            userMock.Setup(u => u.Id).Returns("test-id");

            var storedOffer = new Mock<Offer>(userMock.Object);
            int offerId = storedOfferId;
            storedOffer.Setup(o => o.Id).Returns(offerId);

            var repositoryMock = new Mock<IRepository<Offer>>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), default).Result).Returns(storedOffer.Object);

            var factoryMock = new Mock<IRepositoryFactory>();
            factoryMock.Setup(x => x.CreateRepository<Offer>()).Returns(repositoryMock.Object);

            var fileManagerMock = new Mock<IFileManager>();
            var MediaServiceMock = new Mock<MediaService>(factoryMock.Object, fileManagerMock.Object);
            return (new OfferService(factoryMock.Object, MediaServiceMock.Object), userMock.Object);
        }
    }
}
