namespace Socializer.Services.Data.Tests.Users
{
    using System.Linq;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Data.Repositories;
    using Socializer.Services.Data.Tests.Common;
    using Socializer.Services.Data.Users;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;
    using Xunit;

    public class UserServiceTests : TestBase
    {
        [Fact]
        public async Task GetUserByUsernameReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { FirstName = "Ran1121521", UserName = "Random113513" });
            await db.Users.AddAsync(new ApplicationUser() { FirstName = "Ran11351336326", UserName = "Random1214" });
            await db.Users.AddAsync(new ApplicationUser() { FirstName = "Ran134637", UserName = "Random164574567" });
            await db.Users.AddAsync(new ApplicationUser() { FirstName = "Ran1345458", UserName = "Random1" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetUserByUsernameAsync<TestModel>("Random164574567");
            var correctFirstName = db.Users.FirstOrDefault(x => x.UserName == "Random164574567").FirstName;

            Assert.NotNull(result);
            Assert.Equal(correctFirstName, result.FirstName);
        }

        [Fact]
        public async Task GetUserByUsernameShouldReturnNull()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { FirstName = "Ran1121521", UserName = "Random113513" });
            await db.Users.AddAsync(new ApplicationUser() { FirstName = "Ran11351336326", UserName = "Random1214" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetUserByUsernameAsync<TestModel>("Random16567");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "2", UserName = "Random1214" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "3", UserName = "Random164574567" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "4", UserName = "Random1" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetUserByIdAsync<TestModel>("2");
            var correctFirstName = db.Users.FirstOrDefault(x => x.UserName == "Random1214").Id;

            Assert.NotNull(result);
            Assert.Equal(correctFirstName, result.Id);
        }

        [Fact]
        public async Task GetUserByIdShouldReturnNull()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "2", UserName = "Random1214" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetUserByIdAsync<TestModel>("Random16567");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllUsersReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "2", UserName = "Random1214" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "3", UserName = "Random164574567" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "4", UserName = "Random1" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetAllUsersAsync<TestModel>();

            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task GetIdByUserNameReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "3", UserName = "Random164574567" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "4", UserName = "Random1" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetIdByUserName("Random164574567");

            Assert.NotNull(result);
            Assert.Equal("3", result);
        }

        [Fact]
        public async Task GetIdByUserNameShouldReturnNullIfNotMatch()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513" });
            await db.Users.AddAsync(new ApplicationUser() { Id = "4", UserName = "Random1" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.GetIdByUserName("Random1645745656456547");

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserShouldReturnNullNotUserFound()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var result = await service.UpdateUser(null, "Random1645745656456547");

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserShouldChangeFirstName()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513", FirstName = "Test" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var model = new EditUserProfileInputModel()
            {
                FirstName = "Test2",
                LastName = "Test2",
                Description = "Test2",
                Gender = Gender.Male,
            };

            var result = await service.UpdateUser(model, "1");
            var actualFirstName = db.Users.FirstOrDefault(x => x.Id == "1").FirstName;

            Assert.True(result);
            Assert.Equal(model.FirstName, actualFirstName);
        }

        [Fact]
        public async Task UpdateUserShouldChangeLastName()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513", LastName = "Test" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var model = new EditUserProfileInputModel()
            {
                FirstName = "Test2",
                LastName = "Test2",
                Description = "Test2",
                Gender = Gender.Male,
            };

            var result = await service.UpdateUser(model, "1");
            var actualLastName = db.Users.FirstOrDefault(x => x.Id == "1").LastName;

            Assert.True(result);
            Assert.Equal(model.LastName, actualLastName);
        }

        [Fact]
        public async Task UpdateUserShouldChangeDescription()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513", Description = "Test" });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var model = new EditUserProfileInputModel()
            {
                FirstName = "Test2",
                LastName = "Test2",
                Description = "Test2",
                Gender = Gender.Male,
            };

            var result = await service.UpdateUser(model, "1");
            var actualDescription = db.Users.FirstOrDefault(x => x.Id == "1").Description;

            Assert.True(result);
            Assert.Equal(model.Description, actualDescription);
        }

        [Fact]
        public async Task UpdateUserShouldChangeGender()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "1", UserName = "Random113513", Gender = Gender.Other });
            await db.SaveChangesAsync();

            var userRepo = new EfRepository<ApplicationUser>(db);
            var service = new UserService(null, userRepo, null);

            var model = new EditUserProfileInputModel()
            {
                FirstName = "Test2",
                LastName = "Test2",
                Description = "Test2",
                Gender = Gender.Male,
            };

            var result = await service.UpdateUser(model, "1");
            var actualGender = db.Users.FirstOrDefault(x => x.Id == "1").Gender;

            Assert.True(result);
            Assert.Equal(model.Gender, actualGender);
        }

        public class TestModel : IMapFrom<ApplicationUser>
        {
            public string Id { get; set; }

            public string FirstName { get; set; }
        }
    }
}
