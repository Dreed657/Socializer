namespace Socializer.Data.Models
{
    using System.Collections.Generic;

    using Socializer.Data.Common.Models;

    public class Group : BaseDeletableModel<int>
    {
        public Group()
        {
            this.Members = new HashSet<GroupMember>();
            this.Posts = new HashSet<Post>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<GroupMember> Members { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
