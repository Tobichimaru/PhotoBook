
using System;
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
                Email = "kzabelova@gmail.com",
                Password = Crypto.HashPassword("12345"),
                Role = role,
                RoleId = role.RoleId
            };
            var user2 = new User
            {
                Email = "xsergey@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = role,
                RoleId = role.RoleId
            };

            //initialize profiles
            Profile profile1 = new Profile
            {
                Age = 18,
                FirstName = "Kate",
                LastName = "Zabelova"
            };
            Profile profile2 = new Profile
            {
                Age = 18,
                FirstName = "Serg",
                LastName = "Kulik"
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