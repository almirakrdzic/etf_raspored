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
        
        public User AddedBy(int id)
        {
            var db = new DataLayer.DatabaseEntities();
            var db_book = db.books.Where(boo => boo.id == id).FirstOrDefault();
            int user_id = db_book.added_by;
            var db_user = db.users.Where(u => u.id == user_id).FirstOrDefault();
            var user = new User()
            {                
                FirstName = db_user.first_name,
                LastName = db_user.last_name,
                Type = new UserType()
                {
                    Id = db_user.type
                },
                Username = db_user.username           
               
            };

            return user;
        }


        public Author GetAuthor(int id)
        {
            var db = new DataLayer.DatabaseEntities();
            var dbAuthor = db.authors.Where(au => au.id == id).FirstOrDefault();
            var author = new Author()
            {
                Id = dbAuthor.id,
                FirstName = dbAuthor.first_name,
                LastName = dbAuthor.last_name
            };

            return author;
        }

        public Book GetBookWithId(int id) {

            var db = new DataLayer.DatabaseEntities();
            var dbBook = db.books.Where(boo => boo.id == id).FirstOrDefault();
            var book = new Book()
            {
                Id = dbBook.id,
                Title = dbBook.title,
                ISBN = dbBook.isbn,
                Edition= dbBook.edition,
                Description= dbBook.description
           
            };

            return book;
        }

    }
}