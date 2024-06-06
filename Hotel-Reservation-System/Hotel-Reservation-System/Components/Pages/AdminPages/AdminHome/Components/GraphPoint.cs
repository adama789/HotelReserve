using ApexCharts;
using HotelReservationSystem.Components.Data;

namespace HotelReservationSystem.Components.Pages.AdminPages.AdminHome.Components;

public class GraphPoint
{
    public int Index { get; set; }
    public string Day {  get; set; } = string.Empty;

    public static List<GraphPoint> GeneratePoints(int count)
    {
        var points = new List<GraphPoint>();
        for (int i = count; i > 0; i--)
            points.Add(new GraphPoint { Index = i, Day = i.ToString()});

        return points;
    }

    public static bool IsPointsCorrect(int count)
    {
        if (count <= 0)
            return false;
        return true;
    }

    public static List<GraphPoint> GenerateRoomPoints(DataContext dbContext)
    {
        var currentDateTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var lastDayOfMonth = currentDateTime.AddMonths(1).AddDays(-1);
        DateTimeOffset currentDateTimeOffset;
        
        List<Room> availableRooms;
        var points = new List<GraphPoint>();

        for (int i = 1; i <= lastDayOfMonth.Day; i++)
        {
            currentDateTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, i);
            currentDateTimeOffset = new DateTimeOffset(currentDateTime).ToOffset(TimeSpan.Zero);

            availableRooms = Room.GetAvailableRooms(dbContext, currentDateTimeOffset);

            points.Add(new GraphPoint { Index = availableRooms.Count, Day = currentDateTime.ToString("d") });
        }

        return points;
    }

    public static List<GraphPoint> GenerateReservationPoints(DataContext dbContext)
    {
        var currentDateTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var lastDayOfMonth = currentDateTime.AddMonths(1).AddDays(-1);
        DateTimeOffset currentDateTimeOffset;
        
        List<Reservation> reservationsInSpecificDay;
        var points = new List<GraphPoint>();

        for (int i = 1; i <= lastDayOfMonth.Day; i++)
        {
            currentDateTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, i);
            currentDateTimeOffset = new DateTimeOffset(currentDateTime).ToOffset(TimeSpan.Zero);

            reservationsInSpecificDay = Reservation.GetReservationsBySpecificDay(dbContext, currentDateTimeOffset);

            points.Add(new GraphPoint { Index = reservationsInSpecificDay.Count, Day = currentDateTime.ToString("d") });
        }

        return points;
    }

    public static void SetChartOptions<T>(ApexChartOptions<T> options) where T : class
    {
        options.Colors = ["#2d2f31"];
        options.Chart = new Chart
        {
            Height = "130%",
            Width = "227%",
            Zoom = new Zoom
            {
                Enabled = false
            },
            Toolbar = new Toolbar
            {
                Show = false
            },
            OffsetY = 11,
            OffsetX = -7
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
            },
            Tooltip = new XAxisTooltip
            {
                Enabled = false
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
        options.Tooltip = new Tooltip
        {
            Enabled = true
        };
    }
}
