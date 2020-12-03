namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
