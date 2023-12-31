﻿using Users.Public.Models.Enums;

namespace Users.Endpoint.InputModels
{
    public class AuthorizeUserInput
    {
        public required Guid UserId { get; set; }
        public required string UserToken { get; set; }
        public required Permission RequestedPermission { get; set; }
    }
}
