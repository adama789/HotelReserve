using HotelReservationSystem.Components.Data;
using HotelReservationSystem.Components.Pages.AdminPages.Rooms.Components;
using Microsoft.EntityFrameworkCore;

public class RoomType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }

    public RoomType() { }

    public RoomType(int id, string name, int capacity, decimal pricePerNight)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        PricePerNight = pricePerNight;
    }

    public static async Task<RoomType> GetRoomTypeById(DataContext dbContext, int roomTypeId)
    {
        return await dbContext.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == roomTypeId) ?? new RoomType();
	}

    public static List<RoomType> GetRoomTypes(DataContext dbContext)
    {
        return dbContext.RoomTypes.ToList();
    }

    public static bool IsRoomTypeCorrect(RoomModel roomModel)
	{
		if (roomModel.RoomType.Id >= 1 && roomModel.RoomType.Id <= 4)
			return true;
		return false;
	}
}