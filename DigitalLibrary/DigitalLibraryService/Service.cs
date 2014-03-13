using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.Linq;
using DigitalLibraryService.Contracts;
using DigitalLibraryService.Helpers;
using System.ServiceModel.Activation;

namespace DigitalLibraryService
{
    [AspNetCompatibilityRequirements(
         RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {
        public Contracts.User Login(string username, string password)
        {
            var db =new  DataLayer.DatabaseEntities();
            var currentUser= db.users.Where(user => user.username == username && user.password == password).FirstOrDefault();
            if (currentUser == null)
            {
                throw new Exception("User not found");
            }
            var loggedUser = new User()
            {
                Id = currentUser.id,
                FirstName = currentUser.first_name,
                LastName = currentUser.last_name,
                Type = new UserType()
                {
                    Id = currentUser.type
                },
                Username = username,
                Password = password,
                Salt = currentUser.salt
            };

            return loggedUser;
        }


        public void AddNewUser(User newUser)
        {
            var db = new DataLayer.DatabaseEntities();
            db.users.Add(new DataLayer.user()
            {
                id=newUser.Id,
                username=newUser.Username,
                password=newUser.Password,
                type=newUser.Type.Id,
                first_name=newUser.FirstName,
                email=newUser.Email,
                last_name=newUser.LastName
                
            });
            db.SaveChanges();
        }
    }
}