using Moq;
using System.Text.RegularExpressions;

namespace DigitalGoods.UnitTests
{
    public class MediaHandling
    {
        [Fact]
        public void Path_generating_works_properly()
        {
            var fileExtension = "jpeg";
            var fileFiller = "image";
            var contentType = fileFiller + "/" + fileExtension;
            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(m => m.FileExist(It.IsAny<string>())).Returns(false);
            var datePart = DateTime.Today.ToString("yyyy-MM");
            var sut = new PathGenerator(fileManagerMock.Object);
            var expected = $"{datePart}" + @"\\\w+.\w+." + fileExtension;

            var result = sut.Generate(contentType);

            Assert.Matches(expected, result);
        }
    }
}
