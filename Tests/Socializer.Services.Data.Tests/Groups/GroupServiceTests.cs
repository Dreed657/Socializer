namespace Socializer.Services.Data.Tests.Groups
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Moq;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Data.Repositories;
    using Socializer.Services.Data.Groups;
    using Socializer.Services.Data.Tests.Common;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Groups;
    using Xunit;

    public class GroupServiceTests : TestBase
    {
        [Fact]
        public async Task GetByIdReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group() { Name = "Random1" });
            await db.Groups.AddAsync(new Group() { Name = "Random231" });
            await db.Groups.AddAsync(new Group() { Name = "Random3" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.GetByIdAsync<TestUserModel>(2);

            Assert.Equal("Random231", result.Name);
            Assert.IsType<TestUserModel>(result);
        }

        [Fact]
        public async Task GetAllReturnsCorrect()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group() { Name = "Random1" });
            await db.Groups.AddAsync(new Group() { Name = "Random231" });
            await db.Groups.AddAsync(new Group() { Name = "Random3" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.GetAllAsync<TestUserModel>();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task CreateGroupRequestShouldAddCorrectly()
        {
            var db = GetDatabase();
            var groupCreateRepo = new EfRepository<GroupCreateRequest>(db);
            var service = new GroupService(null, null, groupCreateRepo, null);

            var model = new GroupRequestInputModel()
            {
                Name = "Random123",
            };
            var user = new ApplicationUser();

            var requestsCountBefore = db.GroupCreateRequests.Count();
            var result = await service.CreateGroupRequestAsync(model, user);
            var groupName = db.GroupCreateRequests.FirstOrDefault().Name;

            Assert.True(result);
            Assert.Equal(requestsCountBefore + 1, db.GroupCreateRequests.Count());
            Assert.Equal(model.Name, groupName);
        }

        [Fact]
        public async Task IsMemberInGroupShouldReturnTrueIfInGroup()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group()
            {
                Members = new List<GroupMember>() { new GroupMember() { MemberId = "Random123" } },
            });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.IsMemberInGroup(1, "Random123");

            Assert.True(result);
        }

        [Fact]
        public async Task IsMemberInGroupShouldReturnFalseIfNotInGroup()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group()
            {
                Members = new List<GroupMember>() { new GroupMember() { MemberId = "Random123" } },
            });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.IsMemberInGroup(1, "Random1234");

            Assert.False(result);
        }

        [Fact]
        public async Task IsMemberAdminShouldReturnTrueIfIsAdmin()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group()
            {
                Members = new List<GroupMember>() { new GroupMember() { MemberId = "Random123", Role = GroupRole.Admin } },
            });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.IsMemberAdmin(1, "Random123");

            Assert.True(result);
        }

        [Fact]
        public async Task IsMemberAdminShouldReturnFalseIfIsNotAdmin()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group()
            {
                Members = new List<GroupMember>() { new GroupMember() { MemberId = "Random123", Role = GroupRole.Member } },
            });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.IsMemberAdmin(1, "Random123");

            Assert.False(result);
        }

        [Fact]
        public async Task AddMemberToGroupShouldAddCorrectly()
        {
            var db = GetDatabase();
            await db.Users.AddAsync(new ApplicationUser() { Id = "TestUser" });
            await db.Groups.AddAsync(new Group() { Id = 1, Name = "Group007" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var groupMembersCountBefore = db.Groups.FirstOrDefault(x => x.Id == 1).Members.Count();
            var result = await service.AddMemberToGroupAsync(1, "TestUser");
            var groupNameWithId = db.Groups.FirstOrDefault(x => x.Id == 1).Name;

            Assert.Equal(result, groupNameWithId);
            Assert.Equal(groupMembersCountBefore + 1, db.Groups.FirstOrDefault(x => x.Id == 1).Members.Count());
        }

        [Fact]
        public async Task AddMemberToGroupShouldReturnNullIfNotGroup()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group() { Id = 1, Name = "Group007" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var result = await service.AddMemberToGroupAsync(2, "TestUser");

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateGroupShouldUpdateGroupName()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group() { Id = 1, Name = "Group007", Description = "Test" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var model = new GroupInputModel()
            {
                Name = "Group008",
                Description = "Test",
            };

            var result = await service.UpdateGroup(model, 1, "Test");
            var groupName = db.Groups.FirstOrDefault().Name;

            Assert.Equal("Group008", groupName);
            Assert.NotEqual("Group007", groupName);
        }

        [Fact]
        public async Task UpdateGroupShouldUpdateGroupDescription()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group() { Id = 1, Name = "Group007", Description = "Test" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var model = new GroupInputModel()
            {
                Name = "Group007",
                Description = "Test123",
            };

            var result = await service.UpdateGroup(model, 1, "Test");
            var groupName = db.Groups.FirstOrDefault().Description;

            Assert.Equal("Test123", groupName);
            Assert.NotEqual("Test", groupName);
        }

        [Fact]
        public async Task UpdateGroupShouldReturnFalseIfNotGroup()
        {
            var db = GetDatabase();
            await db.Groups.AddAsync(new Group() { Id = 1, Name = "Group007", Description = "Test" });
            await db.SaveChangesAsync();

            var groupRepo = new EfDeletableEntityRepository<Group>(db);
            var service = new GroupService(null, groupRepo, null, null);

            var model = new GroupInputModel();

            var result = await service.UpdateGroup(model, 2, "Test");

            Assert.False(result);
        }

        public class TestUserModel : IMapFrom<Group>
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
