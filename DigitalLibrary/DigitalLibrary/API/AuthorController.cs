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
    public class AuthorController : ApiController
    {
        // GET api/author
        public List<Author> Get()
        {
            Service service = new Service();
            List<Author> authors = service.GetAllAuthors();
            return authors;
        }

        // GET api/author/5
        public Author Get(int id)
        {
            Service service = new Service();
            Author author = service.GetAuthorWithId(Convert.ToString(id));
            return author;
        }

        // POST api/author
        public HttpResponseMessage Post([FromBody]Author author)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Service service = new Service();
                    service.AddAuthor(author);
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

        // PUT api/author/5
        public HttpResponseMessage Put(int id, [FromBody]Author author)
        {
            try
            {
                Service service = new Service();
                service.UpdateAuthor(author);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/author/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Service service = new Service();
                service.DeleteAuthor(Convert.ToString(id));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
