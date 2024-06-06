using HotelReservationSystem.Components.Data;
using HotelReservationSystem.Components.Pages.AdminPages.Rooms.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Room
{
    public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Capacity { get; set; }
	public decimal PricePerNight { get; set; }

	public Room() { }
    public Room(int id, string name, int capacity, decimal pricePerNight)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        PricePerNight = pricePerNight;
    }

	public static bool DoesRoomIdAlreadyExist(DataContext dbContext, RoomModel roomModel)
	{
		return dbContext.Rooms.Any(r => r.Id == roomModel.Id);
	}

	public static List<Room> GetAvailableRoomsByType(DataContext dbContext, string roomTypeName, DateTimeOffset ?checkInDate, DateTimeOffset ?checkOutDate)
	{
		var roomsWithType = dbContext.Rooms.Where(r => r.Name == roomTypeName).ToList();

		var reservationsInPeriod = dbContext.Reservations
			.Where(r => r.CheckInDate < checkOutDate && r.CheckOutDate > checkInDate)
			.ToList();

		var availableRooms = new List<Room>();
		foreach (var room in roomsWithType)
		{
			bool isRoomAvailable = !reservationsInPeriod.Any(r => r.Room.Id == room.Id);

			if (isRoomAvailable)
				availableRooms.Add(room);
		}

		return availableRooms;
	}

    public static List<Room> GetAvailableRooms(DataContext dbContext, DateTimeOffset? day)
    {
        var roomsWithType = dbContext.Rooms.ToList();

        var reservationsInPeriod = dbContext.Reservations
            .Where(r => r.CheckInDate < day && r.CheckOutDate > day)
            .ToList();

        var availableRooms = new List<Room>();
        foreach (var room in roomsWithType)
        {
            bool isRoomAvailable = !reservationsInPeriod.Any(r => r.Room.Id == room.Id);

            if (isRoomAvailable)
                availableRooms.Add(room);
        }

        return availableRooms;
    }

    public static List<Room> GetRooms(DataContext dbContext)
    {
        var rooms = dbContext.Rooms.ToList();
        return rooms;
    }

    public static async Task AddRoom(DataContext dbContext, RoomType roomType, RoomModel roomModel)
	{
		var room = new Room
		{
			Id = roomModel.Id,
			Name = roomType.Name,
			Capacity = roomType.Capacity,
			PricePerNight = roomType.PricePerNight
		};

		dbContext.Rooms.Add(room);
		await dbContext.SaveChangesAsync();
	}

	public static async Task RemoveRoom(DataContext	dbContext, Room room)
	{
		dbContext.Rooms.Remove(room);
		await dbContext.SaveChangesAsync();
	}

	public static bool IsRoomCorrect(Room room)
	{
		if (room.Id >= 0 && room.Capacity > 0 && room.PricePerNight > 0)
			return true;
		return false;
	}
}