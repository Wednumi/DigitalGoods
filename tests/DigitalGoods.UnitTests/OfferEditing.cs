using DigitalGoods.UnitTests.Mocks;
using Moq;

namespace DigitalGoods.UnitTests
{
    public class OfferEditing
    {
        [Fact]
        public void MediaService_calls_both_save_methods()
        {
            var fileManagerMock = new FileManagerMock();
            var pathGeneratorMock = new Mock<IPathGenerator>();
            pathGeneratorMock.Setup(g => g.Generate(It.IsAny<string>())).Returns("some-string");
            var rollBackContainerMock = new Mock<IRollBackContainer>();
            var repositoryMock = new RepositoryMock<Media>();
            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(f => f.CreateRepository<Media>())
                .Returns(repositoryMock);
            var media = new Media("file-name", "content-type", 1);
            var sut = new MediaService(repositoryFactoryMock.Object, fileManagerMock,
                pathGeneratorMock.Object, rollBackContainerMock.Object);

            sut.SaveAsync(media, It.IsAny<Func<FileStream, Task>>()).Wait();

            Assert.Single(fileManagerMock.Files);
            Assert.Equal(1, repositoryMock.CountAsync().Result);
        }

        [Fact]
        public void MediaService_safely_cancels_saving_when_repository_fails()
        {
            var fileManagerMock = new FileManagerMock();
            var pathGeneratorMock = new Mock<IPathGenerator>();
            pathGeneratorMock.Setup(g => g.Generate(It.IsAny<string>())).Returns("some-string");
            var rollBackContainerMock = new Mock<IRollBackContainer>();
            var failingRepositoryMock = new Mock<IRepository<Media>>();
            failingRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Media>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("simulated repository failer"));
            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(f => f.CreateRepository<Media>())
                .Returns(failingRepositoryMock.Object);
            var media = new Media("file-name", "content-type", 1);
            var sut = new MediaService(repositoryFactoryMock.Object, fileManagerMock,
                pathGeneratorMock.Object, rollBackContainerMock.Object);

            sut.SaveAsync(media, It.IsAny<Func<FileStream, Task>>()).Wait();

            Assert.Empty(fileManagerMock.Files);
        }

        [Fact]
        public void MediaService_safely_cancels_saving_when_fileManager_fails()
        {
            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(f => f.SaveAsync(It.IsAny<string>(), It.IsAny<Func<FileStream, Task>>()))
                .Throws(new Exception("simulated fileManager failer"));
            var pathGeneratorMock = new Mock<IPathGenerator>();
            pathGeneratorMock.Setup(g => g.Generate(It.IsAny<string>())).Returns("some-string");
            var rollBackContainerMock = new Mock<IRollBackContainer>();
            var repositoryMock = new RepositoryMock<Media>();
            var repositoryFactoryMock = new Mock<IRepositoryFactory>();
            repositoryFactoryMock.Setup(f => f.CreateRepository<Media>())
                .Returns(repositoryMock);
            var media = new Media("file-name", "content-type", 1);
            var sut = new MediaService(repositoryFactoryMock.Object, fileManagerMock.Object,
                pathGeneratorMock.Object, rollBackContainerMock.Object);

            sut.SaveAsync(media, It.IsAny<Func<FileStream, Task>>()).Wait();

            Assert.Equal(0, repositoryMock.CountAsync().Result);
        }
    }
}
