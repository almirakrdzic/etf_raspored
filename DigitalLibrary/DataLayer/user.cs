//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public user()
        {
            this.books = new HashSet<book>();
            this.books1 = new HashSet<book>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public byte[] salt { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int type { get; set; }
        public Nullable<bool> active { get; set; }
    
        public virtual ICollection<book> books { get; set; }
        public virtual user_types user_types { get; set; }
        public virtual ICollection<book> books1 { get; set; }
    }
}
