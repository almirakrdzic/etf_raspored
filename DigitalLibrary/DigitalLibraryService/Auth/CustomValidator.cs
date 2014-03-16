using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Web;

namespace CustomValidation
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            var db = new DataLayer.DatabaseEntities();
            var currentUser = db.users.Where(user => user.username == userName && user.password == password).FirstOrDefault();
            if (currentUser == null)
            {
                throw new Exception("User not found");
            }
        }
    }
}