using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Users.Users;

namespace AuthServer.Host.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //depending on the scope accessing the user data.
            if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
            {
                //get user from db (in my case this is by email)
                var user = _userRepository.GetUser(context.Subject.Identity.Name);

                if (user != null)
                {
                    var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                    //set issued claims to return
                    context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                }
            }
            else
            {
                //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                //where and subject was set to my user id.
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    //get user from db (find user by user id)
                    var user = _userRepository.GetUser(userId.Value);

                    // issue the claims for the user
                    if (user != null)
                    {
                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
            }
            return Task.FromResult(0);
        }

        //check if user account is active.
        public Task IsActiveAsync(IsActiveContext context)
        {
            //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
            var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");
            if (!string.IsNullOrWhiteSpace(userId.Value))
            {
                var user = _userRepository.GetUser(userId.Value);
                context.IsActive = user != null;
            }
            return Task.FromResult(0);
        }
    }
}
