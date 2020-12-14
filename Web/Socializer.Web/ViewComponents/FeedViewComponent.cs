namespace Socializer.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    public class FeedViewComponent : ViewComponent
    {
        private readonly IRepository<ApplicationUser> userRepo;

        public FeedViewComponent(IRepository<ApplicationUser> userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var user = await this.userRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id == userId);

            var posts = new List<Post>();

            posts.AddRange(user.Posts.Where(x => x.Group == null && x.Privacy != PrivacyStatus.InProfile));

            foreach (var group in user.Groups)
            {
                posts.AddRange(group.Group.Posts.Where(x => x.Privacy == PrivacyStatus.Public && x.Privacy != PrivacyStatus.InGroup));
            }

            posts = posts.OrderByDescending(x => x.CreatedOn).ToList();

            // TODO: ADD AUTO MAPPER
            var models = posts.Select(x => new PostViewModel()
            {
                Id = x.Id,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                Creator = x.Creator,
                Group = x.Group,
                Image = x.Image,
                CommentsCount = x.Comments.Count,
            });

            return this.View(models);
        }
    }
}
