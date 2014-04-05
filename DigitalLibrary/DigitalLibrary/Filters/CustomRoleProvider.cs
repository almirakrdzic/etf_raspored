using System;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Collections.Generic;
using DigitalLibraryContracts;
namespace DigitalLibrary.Filters
{
    public class CustomRoleProvider : RoleProvider
    {
        private string applicationName;

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (name == null || name.Length == 0)
            {
                name = "CustomRoleProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Role Provider");
            }

            //Initialize the abstract base class.
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                var db = new DataLayer.DatabaseEntities();
                foreach (string u in usernames)
                {


                }       
               
            }
            catch
            {
            }
        }

        public override void CreateRole(string roleName)
        {
            try
            {
                var db = new DataLayer.DatabaseEntities();
                DataLayer.user_types type = new DataLayer.user_types();
                type.name = roleName;
                type.active = true;
                db.user_types.Add(type);
                db.SaveChanges();
                
            }
            catch
            {
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            bool ret = false;
            try
            {
                var db = new DataLayer.DatabaseEntities();
                DataLayer.user_types type = (from t in db.user_types where t.name == roleName select t).FirstOrDefault();
                if (type != null)
                {
                    type.active = false;
                    db.SaveChanges();
                    ret = true;
                }
            }
            catch
            {
            }

            return ret;
        }


        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            List<string> users = new List<string>();
            var db = new DataLayer.DatabaseEntities();
            
            return users.ToArray();
        }

        public override string[] GetAllRoles()
        {
            List<string> roles = new List<string>();
            
            var db = new DataLayer.DatabaseEntities();
            List<DataLayer.user_types> types = db.user_types.ToList();
            foreach (DataLayer.user_types type in types)
                roles.Add(type.name);

            return roles.ToArray();
        }


        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            var db = new DataLayer.DatabaseEntities();
            DataLayer.user user = (from u in db.users where u.username == username select u).FirstOrDefault();
            DataLayer.user_types type = (from t in db.user_types where t.id == user.type select t).FirstOrDefault();
            roles.Add(type.name);         

            return roles.ToArray();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            List<string> users = new List<string>();

           

            return users.ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool isValid = false;
            try
            {
                var db = new DataLayer.DatabaseEntities();
                DataLayer.user user = (from u in db.users where u.username == username select u).FirstOrDefault();
                DataLayer.user_types type = (from t in db.user_types where t.id == user.type select t).FirstOrDefault();
                if (type.name == roleName) isValid = true;
            }
            catch
            {
            }          

            return isValid;
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            try
            {
               
            }
            catch
            {
            }
        }

        public override bool RoleExists(string roleName)
        {
            bool isValid = false;

            var db = new DataLayer.DatabaseEntities();
            List<DataLayer.user_types> types = db.user_types.ToList();
            foreach (DataLayer.user_types type in types)
                if (type.name == roleName) return true;

            return isValid;
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }
 
 
    }
}