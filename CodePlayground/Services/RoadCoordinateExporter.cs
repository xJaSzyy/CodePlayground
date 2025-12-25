using System.Text.Json;
using System.Text.Json.Serialization;
using ClosedXML.Excel;

namespace CodePlayground.Services;

public static class RoadCoordinateExporter
{
    public static void ExportRoadsToExcel(string geoJsonPath, string excelPath)
    {
        var geoJsonContent = File.ReadAllText(geoJsonPath);
        using var doc = JsonDocument.Parse(geoJsonContent);
        var root = doc.RootElement;

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Roads");

        // Заголовки
        ws.Cell(1, 1).Value = "Id";
        ws.Cell(1, 2).Value = "Name";
        ws.Cell(1, 3).Value = "CoordsJson";

        int row = 2;

        if (root.TryGetProperty("features", out var features))
        {
            foreach (var feature in features.EnumerateArray())
            {
                if (!feature.TryGetProperty("geometry", out var geometry))
                    continue;

                if (!geometry.TryGetProperty("type", out var typeProp) ||
                    !geometry.TryGetProperty("coordinates", out var coordsProp))
                    continue;

                var type = typeProp.GetString();
                if (type != "LineString" && type != "MultiLineString")
                    continue;

                // id / name дороги из properties
                string id = "";
                string name = "";

                if (feature.TryGetProperty("properties", out var props))
                {
                    if (props.TryGetProperty("id", out var idProp))
                        id = idProp.GetString() ?? "";
                    if (props.TryGetProperty("name", out var nameProp))
                        name = nameProp.GetString() ?? "";
                }

                var coords = new List<Coordinate>();

                if (type == "LineString")
                {
                    foreach (var c in coordsProp.EnumerateArray())
                    {
                        if (c.GetArrayLength() < 2) continue;
                        coords.Add(new Coordinate
                        {
                            Lon = c[0].GetDouble(),
                            Lat = c[1].GetDouble()
                        });
                    }
                }
                else if (type == "MultiLineString")
                {
                    foreach (var line in coordsProp.EnumerateArray())
                    {
                        foreach (var c in line.EnumerateArray())
                        {
                            if (c.GetArrayLength() < 2) continue;
                            coords.Add(new Coordinate
                            {
                                Lon = c[0].GetDouble(),
                                Lat = c[1].GetDouble()
                            });
                        }
                    }
                }

                if (coords.Count == 0)
                    continue;

                var json = JsonSerializer.Serialize(
                    coords,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = null,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });

                ws.Cell(row, 1).Value = id;
                ws.Cell(row, 2).Value = name;
                ws.Cell(row, 3).Value = json;
                row++;
            }
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(excelPath);
    }
}

public class Coordinate
{
    public double Lon { get; set; }
    public double Lat { get; set; }
}