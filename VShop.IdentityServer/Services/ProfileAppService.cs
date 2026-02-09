using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.Services;

public class ProfileAppService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ProfileAppService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        //id do usuario no IdentityServer
        string id = context.Subject.GetSubjectId();
        
        //localiza o usuário pelo id
        ApplicationUser user = await _userManager.FindByIdAsync(id);
        
        //cria claimsPrincipal para o usuário
        ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
        
        //define uma coleção de claims para o usuário e inclui p sobrenome nome de usuário
        List<Claim> claims = userClaims.Claims.ToList();
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
        
        //se o userManager do identity suportar role
        if (_userManager.SupportsUserRole)
        {
            //obtem lista dos nomes das roels para o user
            IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));

                if (_roleManager.SupportsRoleClaims)
                {
                    IdentityRole identityRole = await _roleManager.FindByIdAsync(role);

                    if (identityRole != null)
                    {
                        claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }
                } 
            }
            context.IssuedClaims = claims;
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        //id do usuario no IdentityServer
        string id = context.Subject.GetSubjectId();
        
        //localiza o usuário pelo id
        ApplicationUser user = await _userManager.FindByIdAsync(id);

        context.IsActive = user is not null;
    }
}