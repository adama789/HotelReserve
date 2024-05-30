using HotelReservationSystem.Components.Pages.AdminPages.Rooms.Components;

public class RoomTypeTests
{
	[Fact]
	public void IsRoomTypeCorrect_ShouldReturnTrue_WhenRoomTypeIsCorrect()
	{
		var roomModel = new RoomModel
		{
			RoomType = new RoomType
			{
				Id = 1,
				Capacity = 1,
				Name = "Jednoosobowy",
				PricePerNight = 100.0M
			}
		};

		var result = RoomType.IsRoomTypeCorrect(roomModel);

		Assert.True(result);
	}

	[Fact]
	public void IsRoomTypeCorrect_ShouldReturnFalse_WhenRoomTypeIsIncorrect()
	{
		var roomModel = new RoomModel { 
			RoomType = new RoomType 
			{ 
				Id = 0, 
				Capacity = 1, 
				Name = "Jednoosobowy", 
				PricePerNight = 100.0M} 
			};

		var result = RoomType.IsRoomTypeCorrect(roomModel);

		Assert.False(result);
	}
}
