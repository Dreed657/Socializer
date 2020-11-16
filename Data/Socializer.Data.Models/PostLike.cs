using Socializer.Data.Common.Models;

namespace Socializer.Data.Models
{
    public class PostLike : BaseDeletableModel<int>
    {
        public int PostId { get; set; }

        public Post Post { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsLiked { get; set; }
    }
}
