public class UserTests
{
	[Fact]
	public void Constructor_SetsPropertiesCorrectly()
	{
		var username = "testuser";
		var email = "testuser@example.com";
		var role = "Admin";

		var user = new User(username, email, role);

		Assert.Equal(username, user.UserName);
		Assert.Equal(email, user.Email);
		Assert.Equal(role, user.Role);
	}

	[Fact]
	public void IsUserCorrect_ShouldReturnTrue_WhenUserIsCorrect()
	{
		var user = new User
		{
			Id = "test",
			Email = "test@test",
			Role = "User"
		};

		var result = User.IsUserCorrect(user);

		Assert.True(result);
	}

	[Fact]
	public void IsUserCorrect_ShouldReturnFalse_WhenUserIsIncorrect()
	{
		var user = new User
		{
			Id = "",
			Email = "test@test",
			Role = "Admin"
		};

		var result = User.IsUserCorrect(user);

		Assert.False(result);
	}
}
