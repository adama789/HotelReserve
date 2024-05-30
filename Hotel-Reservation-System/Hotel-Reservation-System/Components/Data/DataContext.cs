using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Components.Data
{
	public class DataContext : IdentityDbContext<User>
	{
		public DbSet<RoomType> RoomTypes { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Reservation> Reservations { get; set; }

		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.UseSerialColumns();
			CreateDefaultRoomTypes(modelBuilder);

		}

		public void CreateDefaultRoomTypes(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<RoomType>().HasData(
			   new RoomType { Id = 1, Name = "Jednoosobowy", Capacity = 1, PricePerNight = 100 },
			   new RoomType { Id = 2, Name = "Dwuosobowy", Capacity = 2, PricePerNight = 150 },
			   new RoomType { Id = 3, Name = "Trzyosobowy", Capacity = 3, PricePerNight = 200 },
			   new RoomType { Id = 4, Name = "Czteroosobowy", Capacity = 4, PricePerNight = 250 }
			);
        }
    }
}
