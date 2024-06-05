using ApexCharts;
using HotelReservationSystem.Components.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Components.Pages.AdminPages.AdminHome.Components;

public class GraphPoint
{
    public int Index { get; set; }
    public int Day {  get; set; }

    public static List<GraphPoint> GeneratePoints(int count)
    {
        List<GraphPoint> points = new List<GraphPoint>();
        for (int i = count; i > 0; i--)
            points.Add(new GraphPoint { Index = i, Day = i});

        return points;
    }

    public static List<GraphPoint> GenerateRoomPoints(DataContext dbContext)
    {
        var currentDateTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var lastDayOfMonth = currentDateTime.AddMonths(1).AddDays(-1);
        DateTimeOffset? currentDateTimeOffset = new DateTimeOffset(lastDayOfMonth).ToOffset(TimeSpan.Zero);
        List<Room> availableRooms = new();
        List<GraphPoint> points = new List<GraphPoint>();

        for (int i = lastDayOfMonth.Day; i > 0; i--)
        {
            currentDateTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, i);
            currentDateTimeOffset = new DateTimeOffset(currentDateTime).ToOffset(TimeSpan.Zero);

            availableRooms = Room.GetAvailableRoomsByType(dbContext, "Jednoosobowy", currentDateTimeOffset, currentDateTimeOffset);

            points.Add(new GraphPoint { Index = availableRooms.Count, Day = i });
        }

        return points;
    }

    public static void SetChartOptions<T>(ApexChartOptions<T> options) where T : class
    {
        options.Colors = new List<string> { "#2d2f31" };
        options.Chart = new Chart
        {
            Height = "130%",
            Width = "225%",
            Zoom = new Zoom
            {
                Enabled = false
            },
            Toolbar = new Toolbar
            {
                Show = false
            }
        };
        options.Fill = new Fill
        {
            Type = new List<FillType> { FillType.Gradient, FillType.Gradient },
            Gradient = new FillGradient
            {
                ShadeIntensity = 1,
                OpacityFrom = 0.4,
                OpacityTo = 0.9,
            }
        };
        options.Xaxis = new XAxis
        {
            Labels = new XAxisLabels
            {
                Show = false
            }
        };
        options.Yaxis = new List<YAxis>
        {
            new YAxis
            {
                Labels = new YAxisLabels
                {
                    Show = false
                }
            }
        };
    }
}
