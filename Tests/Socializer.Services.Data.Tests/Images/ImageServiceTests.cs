namespace Socializer.Services.Data.Tests.Images
{
    using System.Linq;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Data.Repositories;
    using Socializer.Services.Data.Images;
    using Socializer.Services.Data.Tests.Common;
    using Socializer.Services.Mapping;
    using Xunit;

    public class ImageServiceTests : TestBase
    {
        [Fact]
        public async Task GetAllImageByUserIdReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Images.AddAsync(new Image() { Name = "Random1", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Name = "Random231", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Name = "Random3", CreatorId = "Test123" });
            await db.SaveChangesAsync();

            var imageRepo = new EfRepository<Image>(db);
            var service = new ImagesService(imageRepo);

            var result = await service.GetAllImageByUserId<TestModel>("Test");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllImageByUserIdShouldNullIfNotingMatch()
        {
            var db = GetDatabase();
            await db.Images.AddAsync(new Image() { Name = "Random1", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Name = "Random231", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Name = "Random3", CreatorId = "Test" });
            await db.SaveChangesAsync();

            var imageRepo = new EfRepository<Image>(db);
            var service = new ImagesService(imageRepo);

            var result = await service.GetAllImageByUserId<TestModel>("Test123");

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetImageByNameReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Images.AddAsync(new Image() { Id = 1, Name = "Random1", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Id = 2, Name = "Random231", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Id = 3, Name = "Random3", CreatorId = "Test123" });
            await db.SaveChangesAsync();

            var imageRepo = new EfRepository<Image>(db);
            var service = new ImagesService(imageRepo);

            var result = await service.GetImageByName<TestModel>("Random231");
            var correctId = db.Images.FirstOrDefault(x => x.Name == "Random231").Id;

            Assert.Equal(correctId, result.Id);
        }

        [Fact]
        public async Task GetImageByNameShouldNullIfNotingMatch()
        {
            var db = GetDatabase();
            await db.Images.AddAsync(new Image() { Name = "Random1", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Name = "Random231", CreatorId = "Test" });
            await db.Images.AddAsync(new Image() { Name = "Random3", CreatorId = "Test" });
            await db.SaveChangesAsync();

            var imageRepo = new EfRepository<Image>(db);
            var service = new ImagesService(imageRepo);

            var result = await service.GetImageByName<TestModel>("Random231351513513");

            Assert.Null(result);
        }

        public class TestModel : IMapFrom<Image>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
