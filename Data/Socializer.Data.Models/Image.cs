namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
