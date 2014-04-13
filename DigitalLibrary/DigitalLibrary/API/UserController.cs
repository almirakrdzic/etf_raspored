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
    public class UserController : ApiController
    {
        // GET api/user
        public List<User> Get(string query)
        {
            Service service = new Service();
            List<User> users = service.GetUsers(query);
            return users;
        }

        // GET api/user/5
        public User Get(int id)
        {
            Service service = new Service();
            User user = service.GetUser(Convert.ToString(id));
            return user;
        }

        // POST api/user
        public HttpResponseMessage Post([FromBody]User user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Service service = new Service();
                    service.AddNewUser(user);
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

        // PUT api/user/5
        public HttpResponseMessage Put(int id, [FromBody]User user)
        {
            try
            {
                Service service = new Service();
                service.UpdateUser(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/user/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Service service = new Service();
                service.DeleteUser(Convert.ToString(id));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
