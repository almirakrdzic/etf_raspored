using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.Linq;
using DigitalLibraryContracts;
using DigitalLibraryService.Helpers;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace DigitalLibraryService
{
    [AspNetCompatibilityRequirements(
         RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class Service : IService
    {
        public User Login(string username, string password)
        {
            var db = new DataLayer.DatabaseEntities();
            var currentUser = db.users.Where(user => user.username == username && user.password == password).FirstOrDefault();
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
                    Id = currentUser.type,
                    Name = currentUser.user_types.name
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
                username = newUser.Username,
                password = newUser.Password,
                type = newUser.Type.Id,
                first_name = newUser.FirstName,
                email = newUser.Email,
                last_name = newUser.LastName,
                active = true
                
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
                Username = db_user.username,
                IsActive = true
               
            };

            return user;
        }


        public Author GetAuthor(string authorId)
        {
            var db = new DataLayer.DatabaseEntities();
            var id = int.Parse(authorId);
            var dbAuthor = db.authors.Where(au => au.id == id).FirstOrDefault();
            var author = new Author()
            {
                Id = dbAuthor.id,
                FirstName = dbAuthor.first_name,
                LastName = dbAuthor.last_name
            };

            return author;
        }

        public Book GetBookWithId(string bookId)
        {

            var db = new DataLayer.DatabaseEntities();

            var id = int.Parse(bookId);
            var book = db.books.Where(boo => boo.id == id).FirstOrDefault();
            var newBook = new Book()
            {
                Id = book.id,
                AddedBy = new User()
                {
                    Id = book.added_by
                },
                Description = book.description,
                Edition = book.edition,
                ISBN = book.isbn,
                Title = book.title,
                Content = book.data
            };
            return newBook;
        }

        public Genre GetGenreWithId(string genreId)
        {
            var id = int.Parse(genreId);
            var db = new DataLayer.DatabaseEntities();
            var dbGenre = db.genres.Where(ge => ge.id == id).FirstOrDefault();
            var genre = new Genre()
            {
                Id = dbGenre.id,
                Name = dbGenre.name

            };

            return genre;
        }

        public List<Book> GetBooksForGenre(string genreId)
        {
            List<Book> books = new List<Book>();
            using (var db = new DataLayer.DatabaseEntities())
            {
                var id = int.Parse(genreId);
                var currentGenre = db.genres.Where(genre => genre.id == id).FirstOrDefault();
                if (currentGenre == null)
                {
                    throw new FaultException("Genre with selected ID does not exist!");
                }
                books = currentGenre.books.Select(book => book.ToContract()).ToList();
            }
            return books;
        }


        public Author GetAuthorWithId(string authorId)
        {
            Author newAuthor = null;

            using (var db = new DataLayer.DatabaseEntities())
            {

                var id = int.Parse(authorId);
                var author = db.authors.Where(auth => auth.id == id).FirstOrDefault();
                if (author == null)
                {
                    throw new FaultException("Author with selected ID does not exist!");
                }
                newAuthor = author.ToContract();
            }


            return newAuthor;

        }

        public User GetUser(string userId)
        {
            User newUser = null;

            using (var db = new DataLayer.DatabaseEntities())
            {
                var id = int.Parse(userId);

                var user = db.users.Where(us => us.id == id).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User with selected ID does not exist!");
                }
                newUser = user.ToContract();
            }


            return newUser;


        }

        public List<Book> GetDownloadedBooks(string userId)
        {
            List<Book> downloadedBooks = new List<Book>();
            var db = new DataLayer.DatabaseEntities();

            var id = int.Parse(userId);
            var user = db.users.Where(us => us.id == id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User with selected ID does not exist!");
            }
            downloadedBooks = user.books1.Select(book => book.ToContract()).ToList();
            return downloadedBooks;
        }


        public List<Book> GetBooksForAuthor(string authorId)
        {
            List<Book> books = new List<Book>();
            var db = new DataLayer.DatabaseEntities();
            var id = int.Parse(authorId);
            var author = db.authors.Where(aut => aut.id == id).FirstOrDefault();
            if (author == null)
            {
                throw new Exception("Author with selected ID does not exist!");
            }
            books = author.books.Select(book => book.ToContract()).ToList();
            return books;
        }


        public void Logout(string userId)
        {
            throw new NotImplementedException();
        }

        public List<UserType> GetUserTypes()
        {
            List<UserType> usertypes = new List<UserType>();
            var db = new DataLayer.DatabaseEntities();
            var utypes = db.user_types;
            if (utypes == null)
            {
                throw new Exception("There are no user types present!");
            }
            usertypes = utypes.ToList().Select(utype => Converters.ToContract(utype)).ToList();
            return usertypes;
        }

        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User u)
        {
            throw new NotImplementedException();
        }

        public void AddBook(Book k)
        {
            throw new NotImplementedException();
        }

        public void AddGenre(Genre g)
        {
            throw new NotImplementedException();
        }

        public void AddAuthor(Author a)
        {
            throw new NotImplementedException();
        }

        public void DeleteGenre(string genreId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(string authorId)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(string bookId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetAllGenres()
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public void UpdateGenre(Genre g)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author a)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book b)
        {
            throw new NotImplementedException();
        }

        public List<Book> SearchBooks(string searchString)
        {
            throw new NotImplementedException();
        }


    }


}