﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigitalLibraryContracts
{
     [DataContract]
    public class Genre
    {
         [DataMember]
         public int Id { get;  set; }

         [DataMember]
         public string Name { get; set;  }
    }
}