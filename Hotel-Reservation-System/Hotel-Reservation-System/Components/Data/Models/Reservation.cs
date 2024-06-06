using HotelReservationSystem.Components.Data;
using HotelReservationSystem.Components.Pages.UserPages.UserHome.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    public int Id { get; set; }
	public int RoomId { get; set; }

    [Required]
    public Room Room { get; set; } = new();

    [Required]
    public User User { get; set; } = new();
	public string Customer { get; set; } = "";

    [Required]
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public double TotalPrice { get; set; }

	public static async Task<bool> IsRoomReserved(DataContext dbContext, Room room)
	{
		return await dbContext.Reservations.AnyAsync(r => r.Room.Id == room.Id);
	}

	public static async Task<int> GetNextReservationId(DataContext dbContext)
	{
		int maxId = await dbContext.Reservations.MaxAsync(r => (int?)r.Id) ?? 0;
		return maxId + 1;
	}

	public static async Task ReserveRoom(DataContext dbContext, ReservationModel reservationModel)
	{
		var reservation = new Reservation
		{
			Id = reservationModel.ReservationId,
			Room = reservationModel.ReservationRoom,
			User = reservationModel.User,
			Customer = reservationModel.Customer,
			CheckInDate = reservationModel.CheckInDate,
			CheckOutDate = reservationModel.CheckOutDate,
			TotalPrice = reservationModel.TotalPrice
		};

		dbContext.Reservations.Add(reservation);
		await dbContext.SaveChangesAsync();
	}

    public static IEnumerable<Reservation> GetReservations(DataContext dbContext)
    {
        return dbContext.Reservations;
    }

    public static List<Reservation> GetReservationsList(DataContext dbContext)
    {
        var reservations = dbContext.Reservations.ToList();
        return reservations;
    }

	public static async Task<List<Reservation>> GetUserReservations(DataContext dbContext, User user)
	{
		var reservations = await dbContext.Reservations.Where(r => r.User.Id == user.Id).ToListAsync();
		return reservations;
	}

    public static double CalculateReservationPrice(ReservationModel reservationModel, RoomType roomType)
	{
		double days = Math.Ceiling((reservationModel.CheckOutDate - reservationModel.CheckInDate).TotalDays);
		double totalPrice = Math.Round(days * (double)roomType.PricePerNight, 2) - 0.01;

		return totalPrice;
	}

	public static async Task<int> RemoveOutdatedReservations(DataContext dbContext)
	{
        var outdatedReservations = dbContext.Reservations
            .Where(r => r.CheckOutDate < DateTime.UtcNow)
            .ToList();

		foreach (var r in outdatedReservations)
			await RemoveReservation(dbContext, r);

		return outdatedReservations.Count;
    }

    public static async Task RemoveReservation(DataContext dbContext, Reservation reservation)
    {
        dbContext.Reservations.Remove(reservation);
        await dbContext.SaveChangesAsync();
    }

    public static List<Reservation> GetReservationsBySpecificDay(DataContext dbContext, DateTimeOffset? day)
    {
        var reservationsInPeriod = dbContext.Reservations
            .Where(r => r.CheckInDate < day && r.CheckOutDate > day)
            .ToList();

        return reservationsInPeriod;
    }
}
