﻿namespace Socializer.Services.Data.Groups
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

        public async Task<T> GetById<T>(int id)
        {
            return await this.groupRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await this.groupRepository.All().To<T>().ToListAsync();
        }

        public async Task<T> GetRequestById<T>(int id)
        {
            return await this.groupCreateRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllRequests<T>()
        {
            return await this.groupCreateRepository
                .All()
                .To<T>()
                .ToListAsync();
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

        public async Task ApproveRequest(int requestId)
        {
            var request = await this.groupCreateRepository.All().Where(x => x.Id == requestId).FirstOrDefaultAsync();

            var group = new Group()
            {
                Name = request.Name,
                Description = request.Description,
            };

            group.Members.Add(new GroupMember() { Member = request.Creator, Group = group });
            await this.groupRepository.AddAsync(group);
            await this.groupRepository.SaveChangesAsync();

            request.Status = Status.Approved;
            await this.groupCreateRepository.SaveChangesAsync();
        }

        public async Task DeclineRequest(int requestId)
        {
            var request = await this.groupCreateRepository.All().Where(x => x.Id == requestId).FirstOrDefaultAsync();

            request.Status = Status.Decline;
            await this.groupCreateRepository.SaveChangesAsync();
        }

        public async Task<int> GetRequestsCount()
        {
            return await this.groupCreateRepository.All().CountAsync();
        }
    }
}