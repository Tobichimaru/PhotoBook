using System.Data.Entity;
using System.Web.Helpers;
using ORM.Models;

namespace ORM
{
    public class DbInitializer : CreateDatabaseIfNotExists<EntityModel> //Different modes (4 types)
    {
        protected override void Seed(EntityModel context)
        {
            //initialize roles
            var role = new Role
            {
                Name = "User"
            };
            context.Roles.Add(role);

            var adminRole = new Role
            {
                Name = "Admin"
            };
            context.Roles.Add(adminRole);
            context.Roles.Add(new Role
            {
                Name = "Moderator"
            });
            context.SaveChanges();

            //initialize Users
            var user1 = new User
            {
                Email = "arya.stark@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = role,
                RoleId = role.RoleId,
                UserName = "arya_stark"
            };
            var user2 = new User
            {
                Email = "sansa.stark@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = role,
                RoleId = role.RoleId,
                UserName = "sansa_stark"
            };
            var user = new User
            {
                Email = "admin@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = adminRole,
                RoleId = adminRole.RoleId,
                UserName = "admin"
            };

            //initialize profiles
            var profile1 = new Profile
            {
                Age = 12,
                FirstName = "Arya",
                LastName = "Stark",
                UserName = user1.UserName
            };
            var profile2 = new Profile
            {
                Age = 15,
                FirstName = "Sansa",
                LastName = "Stark",
                UserName = user2.UserName
            };
            var profile = new Profile
            {
                UserName = user.UserName
            };

            context.Profiles.Add(profile1);
            context.Profiles.Add(profile2);
            context.Profiles.Add(profile);
            context.SaveChanges();

            user1.UserProfile = profile1;
            user1.UserProfileId = profile1.Id;
            user2.UserProfile = profile2;
            user2.UserProfileId = profile2.Id;
            user.UserProfile = profile;
            user.UserProfileId = profile.Id;

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}