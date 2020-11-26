namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Mapping;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Web.ViewModels.Groups;

    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> groupRepository;
        private readonly IRepository<GroupCreateRequest> groupCreateRepository;

        public GroupService(IRepository<Group> groupRepository, IRepository<GroupCreateRequest> groupCreteRepository)
        {
            this.groupRepository = groupRepository;
            this.groupCreateRepository = groupCreteRepository;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.groupRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public async Task<bool> AddMemberToGroupAsync(int groupId, string userId)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            if (group != null)
            {
                var entity = new GroupMember()
                {
                    GroupId = groupId,
                    MemberId = userId,
                    Role = GroupRole.Member,
                };

                group.Members.Add(entity);
            }
            else
            {
                return false;
            }

            await this.groupRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.groupRepository.All().To<T>().ToListAsync();
        }

        public async Task<T> GetRequestByIdAsync<T>(int id)
        {
            return await this.groupCreateRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllRequestsAsync<T>()
        {
            return await this.groupCreateRepository
                .All()
                .To<T>()
                .ToListAsync();
        }

        public async Task<bool> CreateGroupRequestAsync(GroupRequestInputModel model, ApplicationUser creator)
        {
            var request = new GroupCreateRequest()
            {
                Creator = creator,
                Name = model.Name,
                Description = model.Description,
                Status = Status.Pending,
            };

            await this.groupCreateRepository.AddAsync(request);
            await this.groupCreateRepository.SaveChangesAsync();

            return true;
        }

        public async Task ApproveRequestAsync(int requestId)
        {
            var request = await this.groupCreateRepository.All()
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(x => x.Id == requestId);

            var group = new Group()
            {
                Name = request.Name,
                Description = request.Description,
            };

            group.Members.Add(new GroupMember() { MemberId = request.Creator.Id, Group = group, Role = GroupRole.Admin });
            await this.groupRepository.AddAsync(group);
            await this.groupRepository.SaveChangesAsync();

            request.Status = Status.Approved;
            await this.groupCreateRepository.SaveChangesAsync();
        }

        public async Task DeclineRequestAsync(int requestId)
        {
            var request = await this.groupCreateRepository.All().Where(x => x.Id == requestId).FirstOrDefaultAsync();

            request.Status = Status.Decline;
            await this.groupCreateRepository.SaveChangesAsync();
        }

        public async Task<int> GetPendingRequestsCountAsync()
        {
            return await this.groupCreateRepository.All().Where(x => x.Status == Status.Pending).CountAsync();
        }

        public async Task<int> GetGroupsCountAsync()
        {
            return await this.groupRepository.All().CountAsync();
        }

        public bool IsMemberInGroup(int groupId, string userId)
        {
            return this.groupRepository
                .All()
                .Include(x => x.Members)
                .FirstOrDefault(x => x.Id == groupId)
                .Members.Any(x => x.MemberId == userId && x.GroupId == groupId);
        }
    }
}
