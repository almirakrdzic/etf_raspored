using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DigitalLibraryContracts;
using DigitalLibraryService;

namespace DigitalLibrary.API
{
    public class BookController : ApiController
    {
        // GET api/book
        public List<Book> Get()
        {
            Service service = new Service();
            List<Book> books = service.GetAllBooks();
            return books;
        }

        // GET api/book/5
        public Book Get(int id)
        {
            Service service = new Service();
            Book book = service.GetBookWithId(Convert.ToString(id));
            return book;

        }

        // GET api/book/5
        public Book Rate(int rate,int id)
        {
            Service service = new Service();
            Book book = service.GetBookWithId(Convert.ToString(id));
            book.Rate = rate;

            return book;

        }

        // POST api/book
        public HttpResponseMessage Post([FromBody]Book book)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Service service = new Service();
                    service.AddBook(book);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // PUT api/book/5
        public HttpResponseMessage Put(int id, [FromBody]Book book)
        {
            try
            {
                Service service = new Service();
                service.UpdateBook(book);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        // DELETE api/book/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Service service = new Service();
                service.DeleteBook(Convert.ToString(id));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
