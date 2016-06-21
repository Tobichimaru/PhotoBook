
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
            context.Roles.Add(new Role
            {
                Name = "Administrator"
            });
            context.Roles.Add(new Role
            {
                Name = "Guest"
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

            //initialize profiles
            Profile profile1 = new Profile
            {
                Age = 12,
                FirstName = "Arya",
                LastName = "Stark",
                UserName = user1.UserName
            };
            Profile profile2 = new Profile
            {
                Age = 15,
                FirstName = "Sansa",
                LastName = "Stark",
                UserName = user2.UserName
            };

            context.Profiles.Add(profile1);
            context.Profiles.Add(profile2);
            context.SaveChanges();

            user1.UserProfile = profile1;
            user1.UserProfileId = profile1.ProfileId;
            user2.UserProfile = profile2;
            user2.UserProfileId = profile2.ProfileId;

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();
        }
    }
}