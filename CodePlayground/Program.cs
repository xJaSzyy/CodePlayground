using CodePlayground.Services;

class Program
{
    private static async Task Main()
    {
        RoadCoordinateExporter.ExportRoadsToExcel("/home/xjasz/RiderProjects/CodePlayground/CodePlayground/GeoJson/export.geojson", "/home/xjasz/Downloads/result.xlsx");
    }
}