using MVC.Authorization;
using MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Data
{
    public static class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {


                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if(user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }
            
            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }


        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contact.Any())
            {
                return;  
            }

            context.Contact.AddRange(
           
                new Contact
                {
                    Nazwa = "Test nazwa1",
                    Miasto = "Test miasto1",
                    Nazwa_Centrum = "Test centrum1",
                    Metraz = "6661",
                    Brygadzista = "Kowalski1",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },

                new Contact
                {
                    Nazwa = "Test nazwa2",
                    Miasto = "Test miasto2",
                    Nazwa_Centrum = "Test centrum2",
                    Metraz = "6662",
                    Brygadzista = "Kowalski2",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },
             new Contact
             {
                 Nazwa = "Test nazwa3",
                 Miasto = "Test miasto3",
                 Nazwa_Centrum = "Test centrum3",
                 Metraz = "6663",
                 Brygadzista = "Kowalski3",
                 Status = ContactStatus.Approved,
                 OwnerID = adminID
             },
             new Contact
             {
                 Nazwa = "Test nazwa4",
                 Miasto = "Test miasto4",
                 Nazwa_Centrum = "Test centrum4",
                 Metraz = "6664",
                 Brygadzista = "Kowalski4",
                 Status = ContactStatus.Approved,
                 OwnerID = adminID
             },
             new Contact
             {
                 Nazwa = "Test nazwa5",
                 Miasto = "Test miasto5",
                 Nazwa_Centrum = "Test centrum5",
                 Metraz = "6665",
                 Brygadzista = "Kowalski5",
                 Status = ContactStatus.Approved,
                 OwnerID = adminID
             }
             );
            context.SaveChanges();
        }
    }
}
