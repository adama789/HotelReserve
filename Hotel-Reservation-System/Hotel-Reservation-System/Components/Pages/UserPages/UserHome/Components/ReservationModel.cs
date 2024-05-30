namespace HotelReservationSystem.Components.Pages.UserPages.UserHome.Components;

public class ReservationModel
{
	public int ReservationId { get; set; }
	public Room ReservationRoom { get; set; } = new();
	public User User { get; set; } = new();
	public DateTime CheckInDate { get; set; }
	public DateTime CheckOutDate { get; set; }
	public string Customer { get; set; } = "";
	public double TotalPrice { get; set; }
}
