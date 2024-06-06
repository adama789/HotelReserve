using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace HotelReservationSystem;

public class SessionController
{
    public string username { get; set; } = string.Empty;

    public async Task<string> GetSessionStorage(ProtectedSessionStorage protectedSessionStore)
    {
        var username = await protectedSessionStore.GetAsync<string>("sessionStorage");

        if (username.Value is not null)
            return username.Success ? username.Value : "";
        return "";
    }

    public bool IsAdminAccessDenied(User ?user)
    {
        if (user?.Role != "Admin" || username == "" || user is null)
            return true;
        return false;
    }

    public bool IsUserAccessDenied(string username)
    {
        if (username == "")
            return true;
        return false;
    }

    public void NavigateToByRole(User user, NavigationManager navigationManager)
    {
        if (user.Role == "User")
            navigationManager.NavigateTo("/user");
        if (user.Role == "Admin")
            navigationManager.NavigateTo("/admin");
    }

    public async Task Logout(ProtectedSessionStorage protectedSessionStore, NavigationManager navigationManager)
    {
        await protectedSessionStore.SetAsync("sessionStorage", "");
        navigationManager.NavigateTo("/");
    }
}
