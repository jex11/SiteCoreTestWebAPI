using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteCoreTestWebAPI.Models.DataTransferObject
{
    public class UserResponse : ApiResponse
    {
        public UserDto AuthorizedUser { get; set; }
    }
}