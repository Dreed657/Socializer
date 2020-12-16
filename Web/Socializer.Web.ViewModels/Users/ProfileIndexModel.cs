namespace Socializer.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using Socializer.Web.ViewModels.Images;

    public class ProfileIndexModel
    {
        public int FriendsCount => this.Friends.Count;

        public int ImagesCount => this.Images.Count;

        public ProfileViewModel ViewModel { get; set; }

        public ICollection<ShortUserViewModel> Friends { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }
    }
}
