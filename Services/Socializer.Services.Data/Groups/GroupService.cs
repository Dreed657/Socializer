using System;
using CloudinaryDotNet;

namespace Socializer.Services.Data.Groups
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
    using Socializer.Web.ViewModels.Groups;

    public class GroupService : IGroupService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository<Group> groupRepository;
        private readonly IRepository<GroupCreateRequest> groupCreateRepository;
        private readonly IRepository<Image> imageRepo;

        public GroupService(Cloudinary cloudinary, IRepository<Group> groupRepository, IRepository<GroupCreateRequest> groupCreteRepository, IRepository<Image> imageRepo)
        {
            this.cloudinary = cloudinary;
            this.groupRepository = groupRepository;
            this.groupCreateRepository = groupCreteRepository;
            this.imageRepo = imageRepo;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.groupRepository.All()
                .Where(x => x.Id == id).To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DbUpdateGroup(DbEditGroupModel model, int groupId)
        {
            var entity = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            if (entity == null)
            {
                return false;
            }

            if (entity.Name != model.Name)
            {
                entity.Name = model.Name;
            }

            if (entity.Description != model.Description)
            {
                entity.Description = model.Description;
            }

            this.groupRepository.Update(entity);
            await this.groupRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsMemberAdmin(int groupId, string userId)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            return group.Members.Any(x => x.MemberId == userId && x.Role == GroupRole.Admin);
        }

        public async Task<string> AddMemberToGroupAsync(int groupId, string userId)
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
                return null;
            }

            await this.groupRepository.SaveChangesAsync();
            return group.Name;
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

            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Name == request.Name);

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

        public async Task<bool> IsMemberInGroup(int groupId, string userId)
        {
            var group = await this.groupRepository
                .All()
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            return group.Members.Any(x => x.MemberId == userId && x.GroupId == groupId);
        }

        public async Task<bool> EditGroup(GroupInputModel model, int groupId, string userId)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            if (group == null)
            {
                return false;
            }

            group.Name = model.Name;
            group.Description = model.Description;

            if (model.CoverImage != null)
            {
                var imageName = Guid.NewGuid().ToString();
                var imageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.CoverImage, imageName);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    group.CoverImage = new Image()
                    {
                        Url = imageUrl,
                        Name = imageName,
                        CreatorId = userId,
                    };
                }
            }

            this.groupRepository.Update(group);
            await this.groupRepository.SaveChangesAsync();

            return true;
        }
    }
}
