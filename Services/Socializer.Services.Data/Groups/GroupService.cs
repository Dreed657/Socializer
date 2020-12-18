namespace Socializer.Services.Data.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
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

        public GroupService(
            Cloudinary cloudinary,
            IRepository<Group> groupRepository,
            IRepository<GroupCreateRequest> groupCreteRepository,
            IRepository<Image> imageRepo)
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

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.groupRepository.All().To<T>().ToListAsync();
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

        public async Task<bool> IsMemberInGroup(int groupId, string userId)
        {
            var group = await this.groupRepository
                .All()
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            return group.Members.Any(x => x.MemberId == userId && x.GroupId == groupId);
        }

        public async Task<bool> IsMemberAdmin(int groupId, string userId)
        {
            var group = await this.groupRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            return group.Members.Any(x => x.MemberId == userId && x.Role == GroupRole.Admin);
        }

        public async Task<bool> UpdateGroup(GroupInputModel model, int groupId, string userId)
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
