namespace Socializer.Services.Data.Tests.Friends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Data.Repositories;
    using Socializer.Services.Data.Friends;
    using Socializer.Services.Data.Tests.Common;
    using Socializer.Services.Mapping;
    using Xunit;

    public class FriendServiceTests : TestBase
    {
        [Fact]
        public async Task GetAllFriendRequestsReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() {Id = "Test1" });
            await db.Users.AddAsync(new ApplicationUser() {Id = "Test2" });
            await db.Users.AddAsync(new ApplicationUser() {Id = "Test3" });
            await db.FriendRequests.AddAsync(new FriendRequest() { SenderId = "Test1", ReceiverId = "Test3", Status = Status.Pending });
            await db.FriendRequests.AddAsync(new FriendRequest() { SenderId = "Test2", ReceiverId = "Test3", Status = Status.Pending });
            await db.SaveChangesAsync();

            var friendRequestRepo = new EfRepository<FriendRequest>(db);
            var service = new FriendService(friendRequestRepo, null, null);

            var result = await service.GetAllFriendRequestsAsync<TestModel>("Test3");

            Assert.Equal(2, result.Count());
        }

        public class TestModel : IMapFrom<FriendRequest>
        {
            public string ReceiverId { get; set; }
        }
    }
}