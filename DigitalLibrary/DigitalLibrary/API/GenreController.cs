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
    public class GenreController : ApiController
    {
        // GET api/genre
        public List<Genre> Get()
        {
            Service service = new Service();
            List<Genre> genres = service.GetAllGenres();
            return genres;
        }

        // GET api/genre/5
        public List<Book> Get(int id)
        {
            List<Book> books = new List<Book>();
            Service service = new Service();
            books = service.GetBooksForGenre(id.ToString());
            return books;
        }       

        // POST api/genre
        public HttpResponseMessage Post([FromBody]Genre genre)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Service service = new Service();
                    service.AddGenre(genre);
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

        // PUT api/genre/5
        public HttpResponseMessage Put(int id, [FromBody]Genre genre)
        {
            try
            {
                Service service = new Service();
                service.UpdateGenre(genre);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/genre/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Service service = new Service();
                service.DeleteGenre(Convert.ToString(id));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
