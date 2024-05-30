namespace HotelReservationSystem.Components.Pages.AdminPages.Rooms.Components;

public class RoomModel
{
	public int Id { get; set; }
	public RoomType RoomType { get; set; } = new();
}
