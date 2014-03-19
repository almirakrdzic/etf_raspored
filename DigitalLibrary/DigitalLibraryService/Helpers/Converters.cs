using DigitalLibraryService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibraryService.Helpers
{
    public static class Converters
    {
        public static Book ToContract(this DataLayer.book book)
        {
            var newBook = new Book()
            {
                Id=book.id,
                AddedBy=new User()
                {
                    Id=book.added_by
                },
                Description=book.description,
                Edition=book.edition,
                ISBN=book.isbn,
                Title=book.isbn,
                Content = book.data
            };
            return newBook;
        }


        public static Author ToContract(this DataLayer.author author)
        {
            Author newAuthor = new Author()
            {
                Id=author.id,
                FirstName=author.first_name,
                LastName=author.last_name
            };
            return newAuthor;
        }

        public static User ToContract(this DataLayer.user user)
        {
            User newUser = new User()
            {
                Id = user.id,
                Username = user.username,
                FirstName = user.first_name,
                LastName = user.last_name,
                Email = user.email

            };
            return newUser;
        }
    }
}