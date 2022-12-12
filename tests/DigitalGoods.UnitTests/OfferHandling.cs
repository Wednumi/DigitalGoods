//using Moq;

//namespace DigitalGoods.UnitTests
//{
//    public class OfferHandling
//    {
//        private (OfferService Service, User Owner) OfferServiceSetup(int storedOfferId)
//        {
//            var userMock = new Mock<User>(It.IsAny<string>(), It.IsAny<string>());
//            userMock.Setup(u => u.Id).Returns("test-id");

//            var storedOffer = new Mock<Offer>(userMock.Object);
//            int offerId = storedOfferId;
//            storedOffer.Setup(o => o.Id).Returns(offerId);

//            var repositoryMock = new Mock<IRepository<Offer>>();
//            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), default).Result).Returns(storedOffer.Object);

//            var factoryMock = new Mock<IRepositoryFactory>();
//            factoryMock.Setup(x => x.CreateRepository<Offer>()).Returns(repositoryMock.Object);

//            var fileManagerMock = new Mock<IFileManager>();
//            var MediaServiceMock = new Mock<MediaService>(factoryMock.Object, fileManagerMock.Object);
//            return (new OfferService(factoryMock.Object, MediaServiceMock.Object), userMock.Object);
//        }
//    }
//}
