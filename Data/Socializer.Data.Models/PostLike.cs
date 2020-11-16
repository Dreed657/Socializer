namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;

    public class PostLike : BaseModel<int>
    {
        public int PostId { get; set; }

        public Post Post { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsLiked { get; set; }
    }
}
