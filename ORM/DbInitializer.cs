
using System;
using System.Data.Entity;
using System.Web.Helpers;
using ORM.Models;

namespace ORM
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EntityModel> //Different modes (4 types)
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

            var photo1 = new Photo
            {
                PublisherUser = user1,
                UserId = user1.UserId,
                Picture = new byte[] {34, 4, 6, 1, 65, 1, 3, 6, 4},
            };

            //initialize profiles
            Profile profile1 = new Profile
            {
                Age = 18,
                FirstName = "Kate",
                LastName = "Zabelova",
                LastUpdateDate = DateTime.Now
            };
            Profile profile2 = new Profile
            {
                Age = 18,
                FirstName = "Serg",
                LastName = "Kulik",
                LastUpdateDate = DateTime.Now
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

            user1.Photos.Add(photo1);
            context.Photos.Add(photo1);
            role.Users.Add(user1);
            role.Users.Add(user2);

            context.SaveChanges();
        }
    }
}