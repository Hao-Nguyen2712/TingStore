using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Users;
using TingStore.Client.Areas.User.Models.UserProfile;

namespace TingStore.Client.Areas.User.Services.UserProfile
{
    public interface IUserProfileService
    {
         Task<UserProfileResponse> GetUserById(int id);
        Task<bool> UpdateUserProfile(UpdateUserProfileRequest user);


    }
}
