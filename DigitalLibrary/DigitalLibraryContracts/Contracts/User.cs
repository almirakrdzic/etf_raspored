using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibraryContracts
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Username is required"), StringLength(50)] 
        public string Username { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Password is required"), StringLength(50)] 
        public string Password { get; set; }
        
        [DataMember]
        public byte[] Salt { get; set; }

        [DataMember]
        [Required(ErrorMessage = "First name is required"), StringLength(50)] 
        public string FirstName { get; set; }
        
        [DataMember]
        [Required(ErrorMessage = "Last name is required"), StringLength(50)] 
        public string LastName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Email is required"), StringLength(50)] 
        public string Email { get; set;  }

        [DataMember]
        public UserType Type { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}