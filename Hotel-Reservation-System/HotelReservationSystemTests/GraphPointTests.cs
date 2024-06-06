using HotelReservationSystem.Components.Pages.AdminPages.AdminHome.Components;

public class GraphPointTests
{
    [Fact]
    public void IsListGraphPointCorrect_ShouldReturnTrue_WhenGraphPointIsCorrect()
    {
        var points = GraphPoint.GeneratePoints(5);

        var result = GraphPoint.IsPointsCorrect(points.Count);

        Assert.True(result);
    }

    [Fact]
    public void IsListGraphPointCorrect_ShouldReturnFalse_WhenGraphPointIsIncorrect()
    {
        var points = GraphPoint.GeneratePoints(0);

        var result = GraphPoint.IsPointsCorrect(points.Count);

        Assert.False(result);
    }
}
