using HotelReservationSystem.Components.Data;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public new string Email { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

    public string Role { get; set; } = string.Empty;

    public User() { }

    public User(string username, string email, string role)
    {
        UserName = username;
        Email = email;
        Role = role;
    }

	public static List<User> GetUsers(UserManager<User> userManager)
	{
		var users = userManager.Users.ToList();
		return users;
	}

    public static List<User> GetUsers(DataContext dbContext)
    {
        var users = dbContext.Users.ToList();
        return users;
    }

	public static bool IsUserCorrect(User user)
	{
		if (user.Id != "" && user.Role is not null && user.Email is not null)
			return true;
		return false;
	}
}
