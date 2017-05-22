using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthServer.Users.Infrastructure;
using AuthServer.Users.Users;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace AuthServer.Host.Infrastructure
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from db
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository; //DI
        }

        //this is used to validate your user account with provided grant at /connect/token
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = _userRepository.GetUserByEmailWithPassword(context.UserName);
                if (user != null)
                {
                    if (Cryptography.ConfirmPassword(context.Password, user.Password, user.Salt))
                    {
                        context.Result = new GrantValidationResult(
                            user.UserId,
                            "custom",
                            GetUserClaims(user));

                        return Task.FromResult(0);
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return Task.FromResult(0);

                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return Task.FromResult(0);

            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "Invalid username or password");
            }

            return Task.FromResult(0);
        }

        //build claims array from user data
        public static Claim[] GetUserClaims(User user)
        {
            return new[]
            {
                new Claim("user_id", user.UserId ?? ""),
                new Claim(JwtClaimTypes.Name,user.Email ?? ""),
                new Claim(JwtClaimTypes.Email, user.Email ?? "")
                //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

                //roles
               // new Claim(JwtClaimTypes.Role, user.Role)
            };
        }
    }
}