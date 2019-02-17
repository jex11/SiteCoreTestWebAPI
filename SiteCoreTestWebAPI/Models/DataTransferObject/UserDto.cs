using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteCoreTestWebAPI.Models.DataTransferObject
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserRoles { get; set; }
    }
}