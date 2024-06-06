using HotelReservationSystem.Components.Pages.AdminPages.AdminHome.Components;

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
    public static List<GraphPoint> GeneratePoints(int count)
    {
        var points = new List<GraphPoint>();
        for (int i = count; i > 0; i--)
            points.Add(new GraphPoint { Index = i, Day = i.ToString() });

        return points;
    }
}