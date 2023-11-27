using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Public.Models.Enums;

namespace Users.Client.Models
{
    public class AuthorizeUserInput
    {
        public required Guid UserId { get; set; }
        public required string UserToken { get; set; }
        public required Permission RequestedPermission { get; set; }
    }
}
