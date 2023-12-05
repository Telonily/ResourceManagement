using PublishedLanguage.Enums;

namespace Users.Client.Models;

public class AuthorizeUserInput
{
    public required Guid UserId { get; set; }
    public required string UserToken { get; set; }
    public required Permission RequestedPermission { get; set; }
}
