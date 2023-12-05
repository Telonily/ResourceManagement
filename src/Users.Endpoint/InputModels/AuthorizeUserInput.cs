using PublishedLanguage.Enums;

namespace Users.Endpoint.InputModels;

// todo: query?
public class AuthorizeUserInput
{
    public required Guid UserId { get; set; }
    public required string UserToken { get; set; }
    public required Permission RequestedPermission { get; set; }
}
