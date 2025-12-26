using CodePlayground.Services;

class Program
{
    private static async Task Main()
    {
        RoadCoordinateExporter.ExportRoadsToExcel("/home/xjasz/RiderProjects/CodePlayground/CodePlayground/GeoJson/1.geojson", "/home/xjasz/Downloads/1.xlsx", 1);
        RoadCoordinateExporter.ExportRoadsToExcel("/home/xjasz/RiderProjects/CodePlayground/CodePlayground/GeoJson/2.geojson", "/home/xjasz/Downloads/2.xlsx", 2);
        RoadCoordinateExporter.ExportRoadsToExcel("/home/xjasz/RiderProjects/CodePlayground/CodePlayground/GeoJson/3.geojson", "/home/xjasz/Downloads/3.xlsx", 3);
        RoadCoordinateExporter.ExportRoadsToExcel("/home/xjasz/RiderProjects/CodePlayground/CodePlayground/GeoJson/4.geojson", "/home/xjasz/Downloads/4.xlsx", 4);
    }
}