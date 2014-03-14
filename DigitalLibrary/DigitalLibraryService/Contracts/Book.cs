using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalLibraryService.Contracts
{
    [DataContract]
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Edition { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public User AddedBy { get; set; }



    }
}