using System.Net.Http.Json;
using Users.Client.Abstract;
using Users.Client.Models;

namespace Users.Client.Concrete;

public class UserService : IUserService
{
    private readonly HttpClient HttpClient;

    public UserService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<bool> AuthorizeUserAsync(AuthorizeUserInput input)
    {
        var response = await HttpClient.PostAsJsonAsync("api/Authorization/AuthorizeUser", input);

        if (response.IsSuccessStatusCode && bool.TryParse(response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult(), out bool result))
            return result;

        throw new InvalidOperationException(response.Content.ToString());
    }

    public bool AuthorizeUser(AuthorizeUserInput input)
        => AuthorizeUserAsync(input).ConfigureAwait(false).GetAwaiter().GetResult();
}
