using HotelReservationSystem.Components;
using HotelReservationSystem.Components.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddBlazorBootstrap();

        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

		builder.Services.AddDefaultIdentity<User>(options =>
            options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<DataContext>();

		var app = builder.Build();

		app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

		app.Run();
    }
}

