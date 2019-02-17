using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteCoreTestWebAPI.Models.DataTransferObject
{
    public class UserRequest
    {
        public string userName { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }
}