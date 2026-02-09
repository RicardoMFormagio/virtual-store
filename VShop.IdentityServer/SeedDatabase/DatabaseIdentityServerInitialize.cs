using System.Security.Claims;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitialize : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        //se o perfil ADMIN não existir então cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            //Cria o perfil admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }
        
        //se o perfil CLIENT não existir então cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            //Cria o perfil admin
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        //Se o usuário ADMIN não existir cria o usuário, define a senha e atrubui ao perfil
        if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            //define os dados do usuário admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.br",
                NormalizedEmail = "ADMIN1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Usuario",
                LastName = "Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            //cria o usuário admin e atribui senha
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsey#2022").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuário admin ao perfil admin
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();
                
                //inclui as claims do usuário admin 
                var adminClaims = _userManager.AddClaimsAsync(admin, [
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                ]).Result;
            }
        }
        
        //Se o usuário ADMIN não existir cria o usuário, define a senha e atrubui ao perfil
        if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            //define os dados do usuário client
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@com.br",
                NormalizedEmail = "CLIENT1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Usuario",
                LastName = "Client",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            //cria o usuário client e atribui senha
            IdentityResult resultClient = _userManager.CreateAsync(client, "Numsey#2022").Result;
            if (resultClient.Succeeded)
            {
                //inclui o usuário client ao perfil client
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();
                
                //inclui as claims do usuário client 
                var clientClaims = _userManager.AddClaimsAsync(client, [
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                ]).Result;
            }
        }
        
        
    }
}