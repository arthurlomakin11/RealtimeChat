using Microsoft.AspNetCore.Identity;
using RealtimeChat.Persistence.DB.Entities;
using System.Security.Claims;

namespace RealtimeChat.API;

public class ExternalAuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
{
    public async Task<ApplicationUser?> HandleExternalLoginAsync(ClaimsPrincipal principal, string provider)
    {
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        var providerKey = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var fullName = principal.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(providerKey))
        {
            return null;
        }

        var (firstName, lastName) = SplitName(fullName);
        var loginInfo = new UserLoginInfo(provider, providerKey, provider);
        
        var user = await userManager.FindByLoginAsync(provider, providerKey);
        user ??= await userManager.FindByEmailAsync(email);
        
        // If user does not exist - create new
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            var createResult = await userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return null;
            }
        }
        
        var existingLogins = await userManager.GetLoginsAsync(user);
        var alreadyLinked = existingLogins
            .Any(l => l.LoginProvider == provider && l.ProviderKey == providerKey);

        if (!alreadyLinked)
        {
            var addLoginResult = await userManager.AddLoginAsync(user, loginInfo);
            if (!addLoginResult.Succeeded)
            {
                return null;
            }
        }

        await signInManager.SignInAsync(user, isPersistent: false);
        return user;
    }

    private static (string?, string?) SplitName(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return (null, null);
        }

        var parts = fullName.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var first = parts.Length > 0 ? parts[0] : null;
        var last = parts.Length > 1 ? parts[1] : null;
        return (first, last);
    }
}