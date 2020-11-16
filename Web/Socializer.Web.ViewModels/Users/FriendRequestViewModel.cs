using System;
using AutoMapper;

namespace Socializer.Web.ViewModels.Users
{
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class FriendRequestViewModel : IMapFrom<FriendRequest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public ApplicationUser Sender { get; set; }

        public ApplicationUser Receiver { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //configuration.CreateMap<FriendRequest, FriendRequestViewModel>()
            //    .ForMember(x => x.FullNameSender,
            //        opt =>
            //            opt.MapFrom(u => $"{u.Sender.FirstName} {u.Sender.LastName}"));

            //configuration.CreateMap<FriendRequest, FriendRequestViewModel>()
            //    .ForMember(x => x.FullNameReceiver,
            //        opt =>
            //            opt.MapFrom(u => $"{u.Receiver.FirstName} {u.Receiver.LastName}"));
        }
    }
}
