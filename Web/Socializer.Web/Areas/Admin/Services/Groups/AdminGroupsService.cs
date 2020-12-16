namespace Socializer.Web.Areas.Admin.Services.Groups
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Dashboard.Groups;

    public class AdminGroupsService : IAdminGroupsService
    {
        private readonly IRepository<GroupCreateRequest> groupCreateRequestsRepo;
        private readonly IRepository<Group> groupRepo;
        private readonly IRepository<Image> imageRepo;

        public AdminGroupsService(IRepository<GroupCreateRequest> groupCreateRequestsRepo, IRepository<Group> groupRepo, IRepository<Image> imageRepo)
        {
            this.groupCreateRequestsRepo = groupCreateRequestsRepo;
            this.groupRepo = groupRepo;
            this.imageRepo = imageRepo;
        }

        public async Task<int> GetGroupsCountAsync()
        {
            return await this.groupCreateRequestsRepo.All().CountAsync();
        }

        public async Task<int> GetPendingRequestsCountAsync()
        {
            return await this.groupCreateRequestsRepo.All().Where(x => x.Status == Status.Pending).CountAsync();
        }

        public async Task<T> GetRequestByIdAsync<T>(int id)
        {
            return await this.groupCreateRequestsRepo.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllRequestsAsync<T>()
        {
            return await this.groupCreateRequestsRepo
                .All()
                .To<T>()
                .ToListAsync();
        }

        public async Task ApproveRequestAsync(int requestId)
        {
            var request = await this.groupCreateRequestsRepo.All()
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(x => x.Id == requestId);

            var group = await this.groupRepo.All().FirstOrDefaultAsync(x => x.Name == request.Name);

            if (group != null)
            {
                return;
            }

            group = new Group()
            {
                Name = request.Name,
                Description = request.Description,
                CoverImage = await this.imageRepo.All().FirstOrDefaultAsync(x => x.Name == "Default_Group_Cover"),
            };

            group.Members.Add(new GroupMember() { MemberId = request.Creator.Id, Group = group, Role = GroupRole.Admin });
            await this.groupRepo.AddAsync(group);
            await this.groupRepo.SaveChangesAsync();

            request.Status = Status.Approved;
            await this.groupCreateRequestsRepo.SaveChangesAsync();
        }

        public async Task DeclineRequestAsync(int requestId)
        {
            var request = await this.groupCreateRequestsRepo.All().Where(x => x.Id == requestId).FirstOrDefaultAsync();

            request.Status = Status.Decline;
            await this.groupCreateRequestsRepo.SaveChangesAsync();
        }

        public async Task<bool> UpdateGroup(DbEditGroupModel model, int groupId)
        {
            var entity = await this.groupRepo.All().FirstOrDefaultAsync(x => x.Id == groupId);

            if (entity == null)
            {
                return false;
            }

            entity.Name = model.Name;
            entity.Description = model.Description;

            this.groupRepo.Update(entity);
            await this.groupRepo.SaveChangesAsync();

            return true;
        }
    }
}
