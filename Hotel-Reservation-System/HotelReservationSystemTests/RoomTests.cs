public class RoomTests
{
	[Fact]
	public void IsRoomCorrect_ShouldReturnTrue_WhenRoomIsCorrect()
	{
		var room = new Room
		{
			Id = 21,
			Capacity = 1,
			Name = "Jednoosobowy",
			PricePerNight = 100.0M
		};

		var result = Room.IsRoomCorrect(room);

		Assert.True(result);
	}

	[Fact]
	public void IsRoomCorrect_ShouldReturnFalse_WhenRoomIsIncorrect()
	{
		var room = new Room
		{
			Id = -1,
			Capacity = 1,
			Name = "Jednoosobowy",
			PricePerNight = 100.0M
		};

		var result = Room.IsRoomCorrect(room);

		Assert.False(result);
	}
}