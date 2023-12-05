using Users.Client.Models;

namespace Users.Client.Abstract;

// TODO Rozważyć oddzielenie abstrakcji od implementacji, czyli wynieść interfejs do innego projektu
public interface IUserService
{
    bool AuthorizeUser(AuthorizeUserInput input);

    Task<bool> AuthorizeUserAsync(AuthorizeUserInput input);
}
