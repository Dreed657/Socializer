using System.Linq;
using Socializer.Data.Models.Enums;

namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
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

        public async Task<bool> CreateGroupRequest(GroupRequestInputModel model, ApplicationUser creator)
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

        public async Task<int> GetRequestsCount()
        {
            return await this.groupCreateRepository.All().CountAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.groupRepository.All().To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllRequests<T>()
        {
            return await this.groupCreateRepository
                .All()
                .Where(x => x.Status == Status.Pending)
                .To<T>()
                .ToListAsync();
        }
    }
}
